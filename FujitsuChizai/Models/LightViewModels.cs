using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class LightListViewModel
    {
        public IEnumerable<LightViewModel> Lights { get; set; }
    }

    public class LightViewModel
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Floor { get; set; }
    }
}