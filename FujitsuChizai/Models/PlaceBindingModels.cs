using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 登録・更新する場所情報を表します。
    /// </summary>
    public class PlaceBindingModel
    {
        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y座標
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
        /// 種類（ Place または Warp ）
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PlaceMarkType Type { get; set; }
        /// <summary>
        /// 接続ID
        /// </summary>
        public int? WarpId { get; set; }
        /// <summary>
        /// 照明ID
        /// </summary>
        public int? LightId { get; set; }
    }

    public static class PlaceBindingExt
    {
        public static PlaceMark ToPlaceMark(this PlaceBindingModel p)
        {
            // 不正な入力防止
            switch (p.Type)
            {
                case PlaceMarkType.Light:
                    p.Name = null;
                    p.WarpId = null;
                    break;
                case PlaceMarkType.Place:
                    p.WarpId = null;
                    p.LightId = null;
                    break;
                case PlaceMarkType.Warp:
                    p.LightId = null;
                    break;
            }
            return new PlaceMark()
            {
                X = p.X,
                Y = p.Y,
                Name = p.Name,
                Floor = p.Floor,
                Type = p.Type,
                WarpId = p.WarpId,
                LightId = p.LightId
            };
        }
    }
}