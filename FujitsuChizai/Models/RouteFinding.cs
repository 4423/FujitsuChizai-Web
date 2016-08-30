using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// Warshall–Floyd Algorithm により最短経路を求めます。
    /// </summary>
    public class RouteFinding : IRouteFinding
    {
        private static ModelContext db = new ModelContext();

        private static int[,] Dist;
        private static int[,] Pred;
        private static readonly int INF = int.MaxValue / 2;

        public bool IsInitialized => Dist != null;


        private void WarshallFloyd(int n)
        {
            // Id=0 は存在しないため1から
            //TODO: 未使用のIDが存在すると動かなくなりそう
            for (int k = 1; k < n; k++)
            {
                for (int i = 1; i < n; i++)
                {
                    if (Dist[i, k] == INF) continue;

                    for (int j = 1; j < n; j++)
                    {
                        int d = Dist[i, k] + Dist[k, j];
                        if (d < Dist[i, j])
                        {
                            Dist[i, j] = d;
                            Pred[i, j] = Pred[k, j];
                        }
                    }
                }
            }                    
        }

        public void InitializeShortestPathTree()
        {
            // ノードの数だけ配列を確保
            var v = db.PlaceMarks.OrderByDescending(x => x.Id).First().Id; // .Last()が使えない
            Dist = new int[v+10, v+10];
            Pred = new int[v+10, v+10];
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v + 10; j++)
                {
                    Dist[i, j] = INF;
                    Pred[i, j] = -1;
                }
                Dist[i, i] = 0; // 自身に対して重み0
            }

            // Placeを除く重み付き無向グラフ
            foreach (var e in db.Edges.Where(x => x.PlaceMark1.Type != PlaceMarkType.Place && x.PlaceMark2.Type != PlaceMarkType.Place))
            {
                Dist[e.PlaceMarkId1, e.PlaceMarkId2] = Dist[e.PlaceMarkId2, e.PlaceMarkId1] = e.Cost;
                Pred[e.PlaceMarkId1, e.PlaceMarkId2] = e.PlaceMarkId1;
                Pred[e.PlaceMarkId2, e.PlaceMarkId1] = e.PlaceMarkId2;
            }

            // 計算
            WarshallFloyd(v);

            // グラフにPlaceを追加
            foreach (var e in db.Edges.Where(x => x.PlaceMark1.Type == PlaceMarkType.Place || x.PlaceMark2.Type == PlaceMarkType.Place))
            {
                Dist[e.PlaceMarkId1, e.PlaceMarkId2] = Dist[e.PlaceMarkId2, e.PlaceMarkId1] = e.Cost;
                Pred[e.PlaceMarkId1, e.PlaceMarkId2] = e.PlaceMarkId1;
                Pred[e.PlaceMarkId2, e.PlaceMarkId1] = e.PlaceMarkId2;
            }
        }

        public IEnumerable<StepViewModel> GenerateSteps(PlaceMark start, PlaceMark goal)
        {
            var steps = new List<StepViewModel>();

            int g = goal.Id;
            int s = Pred[start.Id, goal.Id];
            while (s != -1)
            {
                steps.Add(new StepViewModel()
                {
                    Start = db.PlaceMarks.Find(s).ToPlaceViewModelType(),
                    End = db.PlaceMarks.Find(g).ToPlaceViewModelType(),
                    Cost = Dist[s, g]
                });
                g = s;
                s = Pred[start.Id, s];
            }
            steps.Reverse();
            return steps;
        }

        public IEnumerable<RouteViewModel> SeekRoutes(PlaceMark start, PlaceMark goal)
        {
            if (!IsInitialized)
            {
                InitializeShortestPathTree();
            }

            int d = Dist[start.Id, goal.Id];
            if (d == INF)
            {
                // 経路なし
                yield return null;
            }
            else
            {
                yield return new RouteViewModel()
                {
                    TotalCost = d,
                    Steps = GenerateSteps(start, goal)
                };
            }
        }
    }
}