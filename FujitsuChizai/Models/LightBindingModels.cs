using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 登録・更新する照明情報を表します。
    /// </summary>
    public class LightBindingModel
    {
        /// <summary>
        /// 照明ID
        /// </summary>
        public int LightId { get; set; }
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
    }

    public static class LightBindingExt
    {
        public static PlaceMark ToPlaceMark(this LightBindingModel value)
        {
            return new PlaceMark()
            {
                X = value.X,
                Y = value.Y,
                Floor = value.Floor,
                LightId = value.LightId,
                Type = PlaceMarkType.Light
            };
        }
    }
}