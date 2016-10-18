using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class MapViewModel
    {
        public List<PlaceMark> PlaceMarks { get; set; }
        public Map Map { get; set; }
        public List<PathData> Paths { get; set; }
        public class PathData
        {
            public Edge Edge { get; set; }
            public string D { get; set; }
            public string StrokeDash { get; set; }
        }
    }
}