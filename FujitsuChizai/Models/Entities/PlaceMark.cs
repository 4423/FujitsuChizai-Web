using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// 地図上に位置する経路案内可能な地点を表します。
    /// </summary>
    public class PlaceMark
    {
        /// <summary>
        /// この PlaceMark のID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// この PlaceMark の位置するY座標
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// この PlaceMark の位置するX座標
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// この PlaceMark の位置する階数
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// この PlaceMark の種類
        /// </summary>
        public PlaceMarkType Type { get; set; }
        /// <summary>
        /// Type が Place の場合にのみ格納される場所名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Type が Light の場合にのみ格納される照明ID
        /// </summary>
        public int LightId { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Edge> Edges { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<History> Histories { get; set; }
    }

    public enum PlaceMarkType
    {
        Unknown,
        Light,
        Place,
        Warp
    }
}