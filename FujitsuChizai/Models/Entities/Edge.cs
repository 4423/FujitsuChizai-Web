using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// 2つのPlaceMarkを接続する辺を表します。
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// 1つめの場所ID
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public int PlaceMarkId1 { get; set; }
        /// <summary>
        /// 2つめの場所ID
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public int PlaceMarkId2 { get; set; }
        /// <summary>
        /// 重み
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// 1つめの PlaceMark
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PlaceMarkId1")]
        public virtual PlaceMark PlaceMark1 { get; set; }
        /// <summary>
        /// 2つめの PlaceMark
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PlaceMarkId2")]
        public virtual PlaceMark PlaceMark2 { get; set; }
    }
}