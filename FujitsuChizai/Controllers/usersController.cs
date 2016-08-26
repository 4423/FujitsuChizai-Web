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
    /// ユーザ情報を扱うエンドポイントです。
    /// </summary>
    public class usersController : ErrorHandleableApiController
    {
        private ModelContext db = new ModelContext();

        /// <summary>
        /// すべてのユーザ情報を取得します。
        /// </summary>
        /// <returns>すべてのユーザ情報</returns>
        public UserListViewModel Get()
        {
            var r = new UserListViewModel()
            {
                Users = db.Users
            };
            throw OKResponse(r);
        }

        /// <summary>
        /// 指定されたユーザIDと一致するユーザ情報を取得します。
        /// </summary>
        /// <param name="id">ユーザID</param>
        /// <returns>ユーザIDと一致したユーザ情報</returns>
        public User Get(int id)
        {
            var r = db.Users.Find(id);
            if (r == null)
            {
                throw ErrorResponse(HttpStatusCode.NotFound);
            }
            throw OKResponse(r);
        }

        /// <summary>
        /// ユーザ情報を登録します。
        /// </summary>
        /// <param name="value">登録するユーザ情報</param>
        /// <returns>ユーザIDが含まれる登録後のユーザ情報</returns>
        public User Post([FromBody]RegisterBindingModel value)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    BornIn = value.BornIn,
                    Country = value.Country,
                    Sex = value.Sex,
                    CreatedAt = DateTime.Now
                };
                db.Users.Add(user);
                db.SaveChanges();
                throw OKResponse(user);
            }

            throw ErrorResponse(HttpStatusCode.BadRequest);
        }
    }
}
