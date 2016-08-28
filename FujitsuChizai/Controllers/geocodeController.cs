using FujitsuChizai.Models;
using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FujitsuChizai.Controllers
{
    /// <summary>
    /// 座標位置を扱うエンドポイントです。
    /// </summary>
    public class geocodeController : ErrorHandleableApiController
    {
        private ModelContext db = new ModelContext();

        /// <summary>
        /// 2つの照明位置から現在位置を取得します。
        /// </summary>
        /// <param name="ceilingLightId">天井の照明ID</param>
        /// <param name="floorLightId">床に反射した照明ID</param>
        /// <returns>現在位置の座標</returns>
        public GeocodeViewModel Get(int ceilingLightId, int floorLightId)
        {
            var cl = db.PlaceMarks.SingleOrDefault(x => x.LightId == ceilingLightId);
            var fl = db.PlaceMarks.SingleOrDefault(x => x.LightId == floorLightId);

            if (cl == null || fl == null)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }

            var r = new GeocodeViewModel()
            {
                CeilingLight = cl.ToLightViewModel(),
                FloorLight = fl.ToLightViewModel(),
                Floor = cl.Floor,
                Angle = Convert.ToInt32(Math.Atan2(fl.Y - cl.Y, fl.X - cl.X) * (180/Math.PI)),
                X = (cl.X + fl.X) / 2,
                Y = (cl.Y + fl.Y) / 2
            };
            throw OKResponse(r);
        }
    }
}
