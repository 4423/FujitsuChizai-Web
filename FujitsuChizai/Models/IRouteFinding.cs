using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujitsuChizai.Models
{
    interface IRouteFinding
    {
        /// <summary>
        /// 最短経路木を初期化します。
        /// </summary>
        void InitializeShortestPathTree();

        /// <summary>
        /// 最短経路を求めます。
        /// </summary>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <returns></returns>
        IEnumerable<RouteViewModel> SeekRoutes(PlaceMark start, PlaceMark goal);
    }
}
