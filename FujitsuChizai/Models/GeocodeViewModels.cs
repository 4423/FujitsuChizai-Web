using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class GeocodeViewModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Floor { get; set; }
        public int Angle { get; set; }
        public LightViewModel CeilingLight { get; set; }
        public LightViewModel FloorLight { get; set; }
    }
}