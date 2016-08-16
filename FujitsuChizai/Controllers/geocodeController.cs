using FujitsuChizai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FujitsuChizai.Controllers
{
    public class geocodeController : ApiController
    {
        /// <summary>
        /// 2つの照明位置から現在位置を取得します。
        /// </summary>
        /// <param name="ceilingLightId">天井の照明ID</param>
        /// <param name="floorLightId">床に反射した照明ID</param>
        /// <returns></returns>
        public GeocodeViewModel Get(int ceilingLightId, int floorLightId)
        {
            return new GeocodeViewModel();
        }
    }
}
