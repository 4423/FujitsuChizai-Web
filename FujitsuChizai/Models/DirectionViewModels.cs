using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class DirectionViewModel
    {
        public bool HasRegistered { get; set; }
        public PlaceViewModel Origin { get; set; }
        public PlaceViewModel Destination { get; set; }
        public IEnumerable<RouteViewModel> Routes { get; set; }
    }

    public class RouteViewModel
    {
        public int TotalCost { get; set; }
        public IEnumerable<StepViewModel> Steps { get; set; }
    }

    public class StepViewModel
    {
        public int Cost { get; set; }
        public PlaceViewModel Start { get; set; }
        public PlaceViewModel End { get; set; }
    }
}