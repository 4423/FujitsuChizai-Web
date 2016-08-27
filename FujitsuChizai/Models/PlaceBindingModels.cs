using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class PlaceBindingModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
    }

    public static class PlaceBindingExt
    {
        public static PlaceMark ToPlaceMark(this PlaceBindingModel p)
        {
            return new PlaceMark()
            {
                X = p.X,
                Y = p.Y,
                Name = p.Name,
                Floor = p.Floor,
                Type = PlaceMarkType.Place
            };
        }
    }
}