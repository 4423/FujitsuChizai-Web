using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class RegisterBindingModel
    {
        /// <summary>
        /// 生まれ年
        /// </summary>
        public int BornIn { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Sexes Sex { get; set; }
        /// <summary>
        /// 出身国
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Countries Country { get; set; }
    }
}