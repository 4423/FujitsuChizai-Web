using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace FujitsuChizai.Models
{
    public class ErrorMessageViewModel
    {
        public ErrorMessageViewModel() { }
        public ErrorMessageViewModel(HttpStatusCode code)
        {
            this.Code = code;
            this.Message = code.ToString();
        }

        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
    }
}