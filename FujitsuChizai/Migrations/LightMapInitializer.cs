using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Migrations
{
    public class LightMapInitializer : DropCreateDatabaseIfModelChanges<ModelContext>
    {
        protected override void Seed(ModelContext context)
        {
            #region 地図画像の登録
            context.Maps.AddOrUpdate(
                new Map()
                {
                    Description = "6階",
                    Floor = 6,
                    MapImageFilePath = @"Resources\Map\6F.jpg",
                    Width = 3436,
                    Height = 3616
                });
            #endregion

            #region 照明位置のプロット
            // 照明位置の等差数列
            var horizontalLine = GetArithmeticProgression(310, 145, 21);
            var verticalLine = GetArithmeticProgression(1012, 145, 12);
            var verticalLineEV1 = GetArithmeticProgression(680, 100, 3);
            var verticalLineEV2 = GetArithmeticProgression(2740, 100, 3);

            // 上廊下
            context.FixedPlaceAdd(210, 558);
            context.SemiFixedPlaceAdd(horizontalLine, 558);

            // 下廊下
            context.FixedPlaceAdd(210, 3065);
            context.SemiFixedPlaceAdd(horizontalLine, 3065);

            // 渡り廊下
            context.SemiFixedPlaceAdd(2048, verticalLineEV1);
            context.SemiFixedPlaceAdd(2048, verticalLine);
            context.SemiFixedPlaceAdd(2048, verticalLineEV2);
            #endregion
        }

        private void InitializeTable<T>(DbSet<T> table) where T : class
        {
            table.RemoveRange(table);
        }

        private IEnumerable<int> GetArithmeticProgression(int start, int difference, int maxGenerateNumber)
        {
            for (int i = 1; i <= maxGenerateNumber; i++)
            {
                yield return start + (i - 1) * difference;
            }
        }
    }

    public static class PlaceMarkSeedHelper
    {
        private static int LightId = 601;

        public static void FixedPlaceAdd(this ModelContext context, int x, int y)
        {
            context.PlaceMarks.AddOrUpdate(new PlaceMark()
            {
                X = x,
                Y = y,
                Floor = 6,
                LightId = LightId++,
                Type = PlaceMarkType.Light
            });
        }

        public static void SemiFixedPlaceAdd(this ModelContext context, int x, IEnumerable<int> y)
        {
            context.PlaceMarks.AddOrUpdate(
                y.Select(t => new PlaceMark()
                {
                    X = x,
                    Y = t,
                    Floor = 6,
                    LightId = LightId++,
                    Type = PlaceMarkType.Light
                }).ToArray());
        }

        public static void SemiFixedPlaceAdd(this ModelContext context, IEnumerable<int> x, int y)
        {
            context.PlaceMarks.AddOrUpdate(
                x.Select(t => new PlaceMark()
                {
                    X = t,
                    Y = y,
                    Floor = 6,
                    LightId = LightId++,
                    Type = PlaceMarkType.Light
                }).ToArray());
        }
    }
}