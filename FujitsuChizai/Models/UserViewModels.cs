using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    /// <summary>
    /// 複数のユーザ情報を表します。
    /// </summary>
    public class UserListViewModel
    {
        /// <summary>
        /// ユーザ情報の配列
        /// </summary>
        public IEnumerable<User> Users { get; set; }
    }
}