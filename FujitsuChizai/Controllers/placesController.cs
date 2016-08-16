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
    /// 場所（店舗・部屋・トイレなど）を扱うエンドポイントです。
    /// </summary>
    public class placesController : ApiController
    {
        /// <summary>
        /// すべての場所情報を取得します。
        /// </summary>
        /// <returns></returns>
        public PlaceListViewModel Get()
        {
            return new PlaceListViewModel();
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="keyword">場所を示すキーワード</param>
        /// <returns></returns>
        public PlaceListViewModel Get(string keyword)
        {
            return new PlaceListViewModel();
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="x">x座標地点</param>
        /// <param name="y">y座標地点</param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public PlaceListViewModel Get(int x, int y, int radius = 10)
        {
            return new PlaceListViewModel();
        }

        /// <summary>
        /// 指定されたパラメータに一致する場所情報を取得します。
        /// </summary>
        /// <param name="keyword">場所を示すキーワード</param>
        /// <param name="x">x座標地点</param>
        /// <param name="y">y座標地点</param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public PlaceListViewModel Get(string keyword, int x, int y, int radius = 10)
        {
            return new PlaceListViewModel();
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を取得します。
        /// </summary>
        /// <param name="id">場所ID</param>
        /// <returns></returns>
        public PlaceViewModel Get(int id)
        {
            return new PlaceViewModel();
        }

        /// <summary>
        /// 場所情報を登録します。
        /// </summary>
        /// <param name="value">登録する場所情報</param>
        public void Post([FromBody]PlaceBindingModel value)
        {
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を更新します。
        /// </summary>
        /// <param name="id">場所ID</param>
        /// <param name="value">登録する場所情報</param>
        public void Put(int id, [FromBody]PlaceBindingModel value)
        {
        }

        /// <summary>
        /// 指定された場所IDと一致する場所情報を削除します。
        /// </summary>
        /// <param name="id">場所ID</param>
        public void Delete(int id)
        {
        }
    }
}
