namespace FujitsuChizai.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using FujitsuChizai.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<FujitsuChizai.Models.Entities.ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FujitsuChizai.Models.Entities.ModelContext context)
        {
            //Seed3F(context);
            return;
            #region 照明位置のプロット
            // 照明位置の等差数列
            var horizontalLine = GetArithmeticProgression(1370, 145, 25);
            var verticalLine = GetArithmeticProgression(1012, 145, 12);
            var verticalLineEV1 = GetArithmeticProgression(680, 100, 3);
            var verticalLineEV2 = GetArithmeticProgression(2740, 100, 3);

            // 上廊下
            //context.FixedPlaceAdd(210, 558);
            context.SemiFixedPlaceAdd(horizontalLine, 588);

            // 下廊下
            context.FixedPlaceAdd(210, 3065);
            context.SemiFixedPlaceAdd(horizontalLine, 3065);

            // 渡り廊下
            context.SemiFixedPlaceAdd(2048, verticalLineEV1);
            context.SemiFixedPlaceAdd(2048, verticalLine);
            context.SemiFixedPlaceAdd(2048, verticalLineEV2);
            #endregion

            #region 場所位置のプロット
            var placeLine1 = new[] { 380, 590, 925, 1340, 1770, 2310, 2740, 3040, 3250 };
            var placeLine4 = new[] { 610, 1040, 1510, 1930, 2260, 2500, 2655, 2810, 2965, 3125, 3280 };

            context.SemiFixedPlaceAdd(placeLine1, 315, PlaceMarkType.Place);
            context.SemiFixedPlaceAdd(placeLine4, 3295, PlaceMarkType.Place);
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

        private void Seed3F(ModelContext context)
        {
            PlaceMarkSeedHelper.Floor = 3;
            PlaceMarkSeedHelper.LightId = 301;

            // 上廊下
            context.SemiFixedPlaceAdd(GetArithmeticProgression(1370, 145, 27), 588);
            // 下廊下
            context.SemiFixedPlaceAdd(GetArithmeticProgression(1370, 145, 27), 3080);
            // 左廊下
            context.SemiFixedPlaceAdd(1370, GetArithmeticProgression(746, 145, 16));
            // 右廊下 (EV部分を細かくしたほうがいいかも)
            context.SemiFixedPlaceAdd(4025, GetArithmeticProgression(746, 145, 16));            
        }
    }
}
