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
    /// 複数の照明情報を表します。
    /// </summary>
    public class LightListViewModel
    {
        /// <summary>
        /// 照明情報の配列
        /// </summary>
        public IEnumerable<PlaceMark> Lights { get; set; }
    }
}