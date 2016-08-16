using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class DirectionViewModel
    {
        public bool HasRegistered { get; set; }
        public LightViewModel OriginLight { get; set; }
        public LightViewModel DestinationLight { get; set; }
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
        public LightViewModel StartLight { get; set; }
        public LightViewModel EndLight { get; set; }
    }
}