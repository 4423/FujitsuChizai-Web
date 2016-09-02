using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// エラー情報を表します。
    /// </summary>
    public class ErrorMessageViewModel
    {
        public ErrorMessageViewModel() { }
        public ErrorMessageViewModel(HttpStatusCode code)
        {
            this.Code = code;
            this.Message = code.ToString();
        }

        /// <summary>
        /// HTTPステータスコード
        /// </summary>
        public HttpStatusCode Code { get; set; }
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string Message { get; set; }
    }
}