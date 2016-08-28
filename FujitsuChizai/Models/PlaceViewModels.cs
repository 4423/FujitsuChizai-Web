using FujitsuChizai.Models.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class PlaceListViewModel
    {
        /// <summary>
        /// 場所情報の配列
        /// </summary>
        public IEnumerable<PlaceViewModel> Places { get; set; }
    }

    public class PlaceViewModel
    {
        /// <summary>
        /// 場所ID
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
        /// 場所名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// この場所の種類（ Place または Warp ）
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PlaceMarkType Type { get; set; }
    }

    public static class PlaceViewExt
    {
        public static PlaceViewModel ToPlaceViewModel(this Entities.PlaceMark p)
        {
            return new PlaceViewModel()
            {
                Id = p.Id,
                X = p.X,
                Y = p.Y,
                Name = p.Name,
                Floor = p.Floor,
                Type = p.Type
            };
        }
    }
}