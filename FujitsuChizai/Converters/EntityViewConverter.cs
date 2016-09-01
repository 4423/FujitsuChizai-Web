using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Converters
{
    public static class EntityViewConverter
    {
        public static string ToPathData(this Models.Entities.Edge e)
            => String.Format("M{0},{1}L{2},{3}",
                e.PlaceMark1.X,
                e.PlaceMark1.Y,
                e.PlaceMark2.X,
                e.PlaceMark2.Y
            );
    }
}