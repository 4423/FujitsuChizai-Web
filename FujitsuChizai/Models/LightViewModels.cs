using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class LightListViewModel
    {
        /// <summary>
        /// 照明情報の配列
        /// </summary>
        public IEnumerable<LightViewModel> Lights { get; set; }
    }

    public class LightViewModel
    {
        /// <summary>
        /// 照明ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// X座標地点
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y座標地点
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 階
        /// </summary>
        public int Floor { get; set; }
        /// <summary>
        /// この照明の種類（ Light ）
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PlaceMarkType Type { get; set; }
    }

    public static class LightViewExt
    {
        public static LightViewModel ToLightViewModel(this Entities.PlaceMark p)
        {
            if (p.LightId == null)
            {
                throw new NullReferenceException("LightId の値が null です。");
            }
            return new LightViewModel()
            {
                Id = (int)p.LightId,
                X = p.X,
                Y = p.Y,
                Floor = p.Floor,
                Type = p.Type
            };
        }

        public static PlaceMark ToPlaceMark(this LightViewModel p)
        {
            return new PlaceMark()
            {
                LightId = p.Id,
                X = p.X,
                Y = p.Y,
                Floor = p.Floor,
                Type = p.Type
            };
        }
    }
}