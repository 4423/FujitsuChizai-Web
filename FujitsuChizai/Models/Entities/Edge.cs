using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// 2つのPlaceMarkを接続する辺を表します。
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// 辺の重み
        /// </summary>
        public int Cost { get; set; }

        public virtual PlaceMark PlaceMarkId1 { get; set; }
        public virtual PlaceMark PlaceMarkId2 { get; set; }
    }
}