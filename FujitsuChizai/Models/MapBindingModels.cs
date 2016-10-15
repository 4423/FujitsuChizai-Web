using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class MapBindingModel
    {
        [Required]
        public int Floor { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
    }
}