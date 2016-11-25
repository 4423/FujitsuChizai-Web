using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace FujitsuChizai
{
    public class AnyExceptionHandler : ExceptionHandler
    {
        // 補足されなかった全ての例外情報を特定のjson形式で返す
        public override void Handle(ExceptionHandlerContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            var body = new { code = code,  message = context.Exception.Message };
            var res = context.Request.CreateResponse(code, body);
            context.Result = new ResponseMessageResult(res);
        }
    }
}