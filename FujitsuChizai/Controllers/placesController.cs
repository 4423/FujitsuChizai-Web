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
    /// 場所（店舗・部屋・トイレなど）を扱うエンドポイントです。
    /// </summary>
    public class placesController : ErrorHandleableApiController
    {
        private ModelContext db = new ModelContext();

        private HttpResponseException ResponseCore(List<PlaceMark> p)
        {
            if (p.Count == 0)
            {
                return ErrorResponse(HttpStatusCode.NotFound);
            }

            var r = new PlaceListViewModel()
            {
                Places = p.Select(x => x.ToPlaceViewModel())
            };
            return OKResponse(r);
        }

        /// <summary>
        /// すべての場所情報を取得します。
        /// </summary>
        /// <returns>すべての場所情報</returns>
        public PlaceListViewModel Get()
        {
            var p = db.PlaceMarks.Where(x => x.Type == PlaceMarkType.Place).ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="keyword">場所を示すキーワード</param>
        /// <returns>指定されたパラメータと一致した場所情報</returns>
        public PlaceListViewModel Get(string keyword)
        {
            var p = db.PlaceMarks
                .Where(t => t.Name.Contains(keyword))
                .Where(t => t.Type == PlaceMarkType.Place)
                .ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="x">x座標地点</param>
        /// <param name="y">y座標地点</param>
        /// <param name="radius"></param>
        /// <returns>指定されたパラメータと一致した場所情報</returns>
        public PlaceListViewModel Get(int x, int y, int radius = 10)
        {
            var p = db.PlaceMarks
                .Where(t => t.Type == PlaceMarkType.Place)
                .Where(t => (x - radius) <= t.X && t.X <= (x + radius))
                .Where(t => (y - radius) <= t.Y && t.Y <= (y + radius))
                .ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="keyword">場所を示すキーワード</param>
        /// <param name="x">x座標地点</param>
        /// <param name="y">y座標地点</param>
        /// <param name="radius"></param>
        /// <returns>指定されたパラメータと一致した場所情報</returns>
        public PlaceListViewModel Get(string keyword, int x, int y, int radius = 10)
        {
            var p = db.PlaceMarks
                .Where(t => t.Name.Contains(keyword))
                .Where(t => t.Type == PlaceMarkType.Place)
                .Where(t => (x - radius) <= t.X && t.X <= (x + radius))
                .Where(t => (y - radius) <= t.Y && t.Y <= (y + radius))
                .ToList();
            throw ResponseCore(p);
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を取得します。
        /// </summary>
        /// <param name="id">場所ID</param>
        /// <returns>場所IDと一致した場所情報</returns>
        public PlaceViewModel Get(int id)
        {
            var p = db.PlaceMarks.Find(id);
            if (p == null || p.Type != PlaceMarkType.Place)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }
            throw OKResponse(p.ToPlaceViewModel());
        }

        /// <summary>
        /// 場所情報を登録します。
        /// </summary>
        /// <param name="value">登録する場所情報</param>
        /// <returns>場所IDが含まれる登録後の場所情報</returns>
        public PlaceViewModel Post([FromBody]PlaceBindingModel value)
        {
            if (!ModelState.IsValid)
            {
                throw ErrorResponse(HttpStatusCode.BadRequest);
            }
            var p = value.ToPlaceMark();
            db.PlaceMarks.Add(p);
            db.SaveChanges();
            throw OKResponse(p.ToPlaceViewModel());
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を更新します。
        /// </summary>
        /// <param name="id">場所ID</param>
        /// <param name="value">更新する場所情報</param>
        /// <returns>更新後の場所情報</returns>
        public PlaceViewModel Put(int id, [FromBody]PlaceBindingModel value)
        {
            if (!ModelState.IsValid)
            {
                throw ErrorResponse(HttpStatusCode.BadRequest);
            }

            var p = db.PlaceMarks.Find(id);
            if (p == null || p.Type != PlaceMarkType.Place)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }

            p.X = value.X;
            p.Y = value.Y;
            p.Name = value.Name;
            p.Floor = value.Floor;
            db.SaveChanges();
            throw OKResponse(p.ToPlaceViewModel());
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を削除します。
        /// </summary>
        /// <param name="id">場所ID</param>
        public void Delete(int id)
        {
            var p = db.PlaceMarks.Find(id);
            if (p == null || p.Type != PlaceMarkType.Place)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }
            db.PlaceMarks.Remove(p);
            db.SaveChanges();
            throw ErrorResponse(HttpStatusCode.Accepted);
        }
    }
}
