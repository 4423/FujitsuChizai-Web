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
    public class usersController : ApiController
    {
        /// <summary>
        /// すべてのユーザ情報を取得します。
        /// </summary>
        /// <returns></returns>
        public UserListViewModel Get()
        {
            return new UserListViewModel();
        }

        /// <summary>
        /// 指定されたユーザIDと一致するユーザ情報を取得します。
        /// </summary>
        /// <param name="id">ユーザID</param>
        /// <returns></returns>
        public User Get(int id)
        {
            return new User();
        }

        /// <summary>
        /// ユーザ情報を登録します。
        /// </summary>
        /// <param name="value">登録するユーザ情報</param>
        /// <returns></returns>
        public User Post([FromBody]RegisterBindingModel value)
        {
            return new User();
        }
    }
}
