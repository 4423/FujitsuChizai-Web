using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 案内経路の候補を表します。
    /// </summary>
    public class DirectionViewModel
    {
        /// <summary>
        /// 経路取得要求が記録されたかどうかを示す値
        /// </summary>
        public bool HasRegistered { get; set; }
        /// <summary>
        /// 出発地
        /// </summary>
        public PlaceMark Origin { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public PlaceMark Destination { get; set; }
        /// <summary>
        /// 経路の候補
        /// </summary>
        public IEnumerable<RouteViewModel> Routes { get; set; }
    }

    /// <summary>
    /// 案内経路を表します。
    /// </summary>
    public class RouteViewModel
    {
        /// <summary>
        /// 経路全体のコスト
        /// </summary>
        public int TotalCost { get; set; }
        /// <summary>
        /// 段階的な経路
        /// </summary>
        public IEnumerable<StepViewModel> Steps { get; set; }
    }

    /// <summary>
    /// 経路の最小単位を表します。
    /// </summary>
    public class StepViewModel
    {
        /// <summary>
        /// 経路のコスト
        /// </summary>
        public int Cost { get; set; }
        /// <summary>
        /// 経路の始点
        /// </summary>
        public PlaceMark Start { get; set; }
        /// <summary>
        /// 経路の終点
        /// </summary>
        public PlaceMark End { get; set; }
    }
}