using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 複数の場所情報を表します。
    /// </summary>
    public class PlaceListViewModel
    {
        /// <summary>
        /// 場所情報の配列
        /// </summary>
        public IEnumerable<PlaceMark> Places { get; set; }
    }
}