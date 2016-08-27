using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// 案内経路の履歴を表します。
    /// </summary>
    public class History
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public int PlaceMarkId { get; set; }

        public virtual User User { get; set; }
        public virtual PlaceMark PlaceMark { get; set; }
    }
}