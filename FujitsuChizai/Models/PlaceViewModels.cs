using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class PlaceListViewModel
    {
        public IEnumerable<PlaceViewModel> Places { get; set; }
    }

    public class PlaceViewModel
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Floor { get; set; }
        public string Name { get; set; }
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
                Floor = p.Floor
            };
        }
    }
}