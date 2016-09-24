using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Converters
{
    public static class EntityViewConverter
    {
        public static string ToPathD(this Models.Entities.Edge e)
            => String.Format("M{0},{1}L{2},{3}",
                e.PlaceMark1.X,
                e.PlaceMark1.Y,
                e.PlaceMark2.X,
                e.PlaceMark2.Y
            );

        public static string ToStrokeDash(this Models.Entities.Edge e)
        {
            double totalLen = Math.Sqrt(Math.Pow(e.PlaceMark1.X - e.PlaceMark2.X, 2) + Math.Pow(e.PlaceMark1.Y - e.PlaceMark2.Y, 2));
            return String.Format("0 20 {0} 20", totalLen - 20 * 2);
        }
    }
}