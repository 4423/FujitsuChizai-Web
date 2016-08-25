using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// 2つのPlaceMarkを接続する辺を表します。
    /// </summary>
    public class Edge
    {
        [Key]
        [Column(Order = 0)]
        public int PlaceMarkId1 { get; set; }
        [Key]
        [Column(Order = 1)]
        public int PlaceMarkId2 { get; set; }
        /// <summary>
        /// 辺の重み
        /// </summary>
        public int Cost { get; set; }
        
        public virtual PlaceMark PlaceMark1 { get; set; }
        public virtual PlaceMark PlaceMark2 { get; set; }
    }
}