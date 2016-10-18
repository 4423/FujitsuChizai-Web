using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FujitsuChizai.Models;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Controllers
{
    /// <summary>
    /// 案内経路を扱うエンドポイントです。
    /// </summary>
    public class directionsController : ErrorHandleableApiController
    {
        ModelContext db = new ModelContext();
        IRouteFinding rf = new RouteFinding();
        private string acceptLanguage => Request.Headers.AcceptLanguage.First().Value;

        private bool AddHistory(int originId, int? userId)
        {
            if (userId == null)
            {
                return false;
            }

            if (db.Users.Find(userId) == null)
            {
                // ユーザが見つからなくても無視して return false すべき？
                throw ErrorResponse(HttpStatusCode.BadRequest);
            }

            db.Histories.Add(new History()
            {
                PlaceMarkId = originId,
                UserId = (int)userId,
                Timestamp = DateTime.Now
            });
            db.SaveChanges();
            return true;
        }

        private DirectionViewModel GetCore(int originId, int destinationId, PlaceMarkType originType, PlaceMarkType destinationType, int? userId)
        {
            var origin = originType == PlaceMarkType.Light ?
                db.PlaceMarks.SingleOrDefault(x => x.LightId == originId) :
                db.PlaceMarks.Find(originId);

            var dest = destinationType == PlaceMarkType.Light ?
                db.PlaceMarks.SingleOrDefault(x => x.LightId == destinationId) :
                db.PlaceMarks.Find(destinationId);

            if (origin == null || dest == null)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }

            var registered = AddHistory(origin.Id, userId);

            origin = origin.Translate(acceptLanguage);
            dest = dest.Translate(acceptLanguage);

            var r = new DirectionViewModel()
            {
                HasRegistered = registered,
                Origin = origin,
                Destination = dest,
                Routes = rf.SeekRoutes(origin, dest)
            };
            throw OKResponse(r);
        }

        /// <summary>
        /// 2点間の案内経路を取得します。
        /// </summary>
        /// <param name="originId">始点となる照明IDまたは場所ID（既定は照明ID）</param>
        /// <param name="destinationId">終点となる照明IDまたは場所ID（既定は場所ID）</param>
        /// <param name="originType">始点の種類</param>
        /// <param name="destinationType">終点の種類</param>
        /// <returns>2点間の案内経路</returns>
        public DirectionViewModel Get(int originId, int destinationId, PlaceMarkType originType = PlaceMarkType.Light, PlaceMarkType destinationType = PlaceMarkType.Place)
        {
            return GetCore(originId, destinationId, originType, destinationType, null);
        }

        /// <summary>
        /// 2点間の案内経路を取得します。
        /// userId を指定した場合、この要求は記録されます。
        /// </summary>
        /// <param name="originId">始点となる照明光IDまたは場所ID（既定は照明ID）</param>
        /// <param name="destinationId">終点となる照明光IDまたは場所ID（既定は場所ID）</param>
        /// <param name="userId">ユーザID</param>
        /// <param name="originType">始点の種類</param>
        /// <param name="destinationType">終点の種類</param>
        /// <returns>2点間の案内経路</returns>
        public DirectionViewModel Get(int originId, int destinationId, int userId, PlaceMarkType originType = PlaceMarkType.Light, PlaceMarkType destinationType = PlaceMarkType.Place)
        {
            return GetCore(originId, destinationId, originType, destinationType, userId);
        }
    }
}
