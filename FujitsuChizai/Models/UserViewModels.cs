using FujitsuChizai.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}