using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    public class Map
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        public string Description { get; set; }
        public string MapImageFilePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}