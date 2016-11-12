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
        private static readonly int PLACE_COST = 10000; // Placeを経由しない程度の重み

        public bool IsInitialized => Dist != null;

        // Singleton
        private RouteFinding() { }
        private static RouteFinding instance;
        public static RouteFinding Instance
            => instance ?? (instance = new RouteFinding());


        public void RequestInitialization()
        {
            Dist = Pred = null;
        }

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
            var v = 1 + db.PlaceMarks.OrderByDescending(x => x.Id).First().Id; // .Last()が使えない
            Dist = new int[v, v];
            Pred = new int[v, v];
            for (int i = 1; i < v; i++)
            {
                for (int j = 1; j < v; j++)
                {
                    Dist[i, j] = INF;
                    Pred[i, j] = -1;
                }
                Dist[i, i] = 0; // 自身に対して重み0
            }

            var edges = db.Edges.Select(x =>
                new
                {
                    Id1 = x.PlaceMarkId1,
                    Id2 = x.PlaceMarkId2,
                    Cost = x.Cost,
                    IsPlaceEdge = x.PlaceMark1.Type == PlaceMarkType.Place || x.PlaceMark2.Type == PlaceMarkType.Place
                });
            // 重み付き無向グラフ構築
            foreach (var e in edges)
            {
                Dist[e.Id1, e.Id2] = Dist[e.Id2, e.Id1] = e.IsPlaceEdge ? PLACE_COST : e.Cost; // Placeを含む辺ならPLACE_COSTが重み
                Pred[e.Id1, e.Id2] = e.Id1;
                Pred[e.Id2, e.Id1] = e.Id2;
            }

            // 同じ WarpId を持つ Warp 同士を接続する
            var group = db.PlaceMarks.Where(x => x.Type == PlaceMarkType.Warp).GroupBy(x => x.WarpId);
            foreach (var pair in group)
            {
                int n = pair.Count();
                for (int i = 0; i < n; i++)
                {
                    var w1 = pair.ElementAt(i);
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) continue;
                        var w2 = pair.ElementAt(j);
                        Dist[w1.Id, w2.Id] = Dist[w2.Id, w1.Id] = 0;
                        Pred[w1.Id, w2.Id] = w1.Id;
                        Pred[w2.Id, w1.Id] = w2.Id;
                    }
                }
            }

            // 計算
            WarshallFloyd(v);
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
                    Start = db.PlaceMarks.Find(s),
                    End = db.PlaceMarks.Find(g),
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