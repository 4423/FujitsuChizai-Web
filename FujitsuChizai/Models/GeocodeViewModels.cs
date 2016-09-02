using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 詳細な現在位置を表します。
    /// </summary>
    public class GeocodeViewModel
    {
        /// <summary>
        /// X座標位置
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y座標位置
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 階
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// 方向角度（-180～180）
        /// </summary>
        public int Angle { get; set; }
        /// <summary>
        /// 天井の照明
        /// </summary>
        public PlaceMark CeilingLight { get; set; }
        /// <summary>
        /// 床に反射した照明
        /// </summary>
        public PlaceMark FloorLight { get; set; }
    }
}