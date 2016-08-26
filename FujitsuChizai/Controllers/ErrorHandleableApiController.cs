using FujitsuChizai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FujitsuChizai.Controllers
{
    public class ErrorHandleableApiController : ApiController
    {
        protected HttpResponseException OKResponse(object response)
            => new HttpResponseException(Request.CreateResponse(HttpStatusCode.OK, response));

        protected HttpResponseException ErrorResponse(HttpStatusCode code)
            => new HttpResponseException(Request.CreateResponse(code, new ErrorMessageViewModel(code)));
    }
}