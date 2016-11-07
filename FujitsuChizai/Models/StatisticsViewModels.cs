using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujitsuChizai.Models.Entities;
using System.Runtime.Serialization;

namespace FujitsuChizai.Models
{
    public class StatisticsViewModel
    {
        public IEnumerable<Map> Maps { get; set; }
        public int MapId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatisticsData Data { get; set; }
    }

    [DataContract]
    public class DataProvider
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public int Value { get; set; }
    }

    public class PlaceMarkFreq
    {
        public PlaceMark PlaceMark { get; set; }
        public int Count { get; set; }
    }

    public class StatisticsData
    {
        public List<PlaceMarkFreq> Origin { get; set; }
        public List<PlaceMarkFreq> Dest { get; set; }
        public List<DataProvider> Country { get; set; }
        public List<DataProvider> Age { get; set; }
        public List<DataProvider> Sex { get; set; }
    }
}