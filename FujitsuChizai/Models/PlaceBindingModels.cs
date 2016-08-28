using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class PlaceBindingModel
    {
        /// <summary>
        /// X座標地点
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y座標地点
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 階
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 場所名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// この場所の種類（ Place または Warp ）
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PlaceMarkType Type { get; set; }
    }

    public static class PlaceBindingExt
    {
        public static PlaceMark ToPlaceMark(this PlaceBindingModel p)
        {
            return new PlaceMark()
            {
                X = p.X,
                Y = p.Y,
                Name = p.Name,
                Floor = p.Floor,
                Type = p.Type
            };
        }
    }
}