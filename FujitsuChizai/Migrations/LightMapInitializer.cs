using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Migrations
{
    public class LightMapInitializer : CreateDatabaseIfNotExists<ModelContext>
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
}