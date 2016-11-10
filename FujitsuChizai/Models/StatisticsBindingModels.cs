using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujitsuChizai.Models
{
    public class StatisticsBindingModel
    {
        public int MapId { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public TimeSpan TimeBegin { get; set; }
        public TimeSpan TimeEnd { get; set; }

        private string _DateRange;
        public string DateRange
        {
            get { return _DateRange; }
            set
            {
                _DateRange = value;
                DateBegin = DateTime.Parse(value.Split('-')[0]);
                DateEnd = DateTime.Parse(value.Split('-')[1]);
            }
        }
    }
}