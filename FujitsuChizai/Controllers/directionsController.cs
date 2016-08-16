using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FujitsuChizai.Models;

namespace FujitsuChizai.Controllers
{
    /// <summary>
    /// 案内経路を扱うエンドポイントです。
    /// </summary>
    public class directionsController : ApiController
    {
        /// <summary>
        /// 2点間の案内経路を取得します。
        /// </summary>
        /// <param name="originId">出発点の照明光IDまたは場所ID</param>
        /// <param name="destinationId">到達点の照明光IDまたは場所ID</param>
        /// <returns></returns>
        public DirectionViewModel Get(int originId, int destinationId)
        {
            return new DirectionViewModel();
        }

        /// <summary>
        /// 2点間の案内経路を取得します。
        /// userId を指定した場合、この要求は記録されます。
        /// </summary>
        /// <param name="originId">出発点の照明光IDまたは場所ID</param>
        /// <param name="destinationId">到達点の照明光IDまたは場所ID</param>
        /// <param name="userId">ユーザID</param>
        /// <returns></returns>
        public DirectionViewModel Get(int originId, int destinationId, int userId)
        {
            return new DirectionViewModel();
        }
    }
}
