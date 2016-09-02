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
    /// 照明を扱うエンドポイントです。
    /// </summary>
    public class lightsController : ErrorHandleableApiController
    {
        private ModelContext db = new ModelContext();

        private HttpResponseException ResponseCore(List<PlaceMark> p)
        {
            if (p.Count == 0)
            {
                return ErrorResponse(HttpStatusCode.NotFound);
            }

            var r = new LightListViewModel()
            {
                Lights = p
            };
            return OKResponse(r);
        }

        /// <summary>
        /// すべての照明情報を取得します。
        /// </summary>
        /// <returns>すべての照明情報</returns>
        public LightListViewModel Get()
        {
            var p = db.PlaceMarks.Where(x => x.Type == PlaceMarkType.Light).ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定されたパラメータに一致する照明情報を取得します。
        /// </summary>
        /// <param name="floor">階</param>
        /// <returns>指定されたパラメータと一致した照明情報</returns>
        public LightListViewModel GetFloor(int floor)
        {
            var p = db.PlaceMarks
                .Where(t => t.Type == PlaceMarkType.Light)
                .Where(t => t.Floor == floor)
                .ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定されたパラメータに一致する照明情報を取得します。
        /// </summary>
        /// <param name="x">X座標地点</param>
        /// <param name="y">Y座標地点</param>
        /// <param name="floor">階</param>
        /// <param name="radius">半径</param>
        /// <returns>指定されたパラメータと一致した照明情報</returns>
        public LightListViewModel Get(int x, int y, int floor, int radius = 10)
        {
            var p = db.PlaceMarks
                .Where(t => t.Type == PlaceMarkType.Light)
                .Where(t => t.Floor == floor)
                .Where(t => (x - radius) <= t.X && t.X <= (x + radius))
                .Where(t => (y - radius) <= t.Y && t.Y <= (y + radius))
                .ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を取得します。
        /// </summary>
        /// <param name="id">照明ID</param>
        /// <returns>照明IDと一致した場所情報</returns>
        public PlaceMark Get(int id)
        {
            var p = db.PlaceMarks.SingleOrDefault(x => x.LightId == id);
            if (p == null || p.Type != PlaceMarkType.Light)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }
            throw OKResponse(p);
        }

        /// <summary>
        /// 照明情報を登録します。
        /// </summary>
        /// <param name="value">登録する照明情報</param>
        /// <returns>登録後の照明情報</returns>
        public PlaceMark Post([FromBody]LightBindingModel value)
        {
            if (!ModelState.IsValid)
            {
                throw ErrorResponse(HttpStatusCode.BadRequest);
            }
            var p = value.ToPlaceMark();
            db.PlaceMarks.Add(p);
            db.SaveChanges();
            throw OKResponse(p);
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を更新します。
        /// </summary>
        /// <param name="id">照明ID</param>
        /// <param name="value">更新する照明情報</param>
        /// <returns>更新後の照明情報</returns>
        public PlaceMark Put(int id, [FromBody]LightBindingModel value)
        {
            if (!ModelState.IsValid)
            {
                throw ErrorResponse(HttpStatusCode.BadRequest);
            }

            var p = db.PlaceMarks.SingleOrDefault(x => x.LightId == id);
            if (p == null || p.Type != PlaceMarkType.Light)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }

            p.X = value.X;
            p.Y = value.Y;
            p.Floor = value.Floor;
            p.LightId = value.LightId;
            db.SaveChanges();
            throw OKResponse(p);
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を削除します。
        /// </summary>
        /// <param name="id">照明ID</param>
        public void Delete(int id)
        {
            var p = db.PlaceMarks.SingleOrDefault(x => x.LightId == id);
            if (p == null || p.Type != PlaceMarkType.Light)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }
            db.PlaceMarks.Remove(p);
            db.SaveChanges();
            throw ErrorResponse(HttpStatusCode.Accepted);
        }
    }
}
