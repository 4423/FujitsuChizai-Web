using FujitsuChizai.Models;
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
    public class lightsController : ApiController
    {
        /// <summary>
        /// すべての照明情報を取得します。
        /// </summary>
        /// <returns></returns>
        public LightListViewModel Get()
        {
            return new LightListViewModel();
        }

        /// <summary>
        /// 指定されたパラメータに一致する照明情報を取得します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public LightListViewModel Get(int x, int y, int radius = 10)
        {
            return new LightListViewModel();
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を取得します。
        /// </summary>
        /// <param name="id">照明ID</param>
        /// <returns></returns>
        public LightViewModel Get(int id)
        {
            return new LightViewModel();
        }

        /// <summary>
        /// 照明情報を登録します。
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]LightViewModel value)
        {
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を更新します。
        /// </summary>
        /// <param name="id">照明ID</param>
        /// <param name="value"></param>
        public void Put(int id, [FromBody]LightViewModel value)
        {
        }

        /// <summary>
        /// 指定された照明IDと一致する照明情報を削除します。
        /// </summary>
        /// <param name="id">照明ID</param>
        public void Delete(int id)
        {
        }
    }
}
