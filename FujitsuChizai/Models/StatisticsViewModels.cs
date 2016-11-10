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
        public bool RequestAllFloors { get; set; }
        public bool HasNoContent { get; set; }
        public List<Map> Maps { get; set; }
        public Map RequestedMap { get; set; }
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

    public static class StatisticsViewModelExtension
    {
        public static IEnumerable<PlaceMarkFreq> ToPlaceMarkFreq(this IEnumerable<IGrouping<PlaceMark, History>> group)
            => group.Select(g => new PlaceMarkFreq() { PlaceMark = g.Key, Count = g.Count() });

        public static IEnumerable<PlaceMarkFreq> ToPlaceMarkFreqDesc(this IEnumerable<IGrouping<PlaceMark, History>> group)
            => group.ToPlaceMarkFreq().OrderByDescending(x => x.Count);

        public static IEnumerable<DataProvider> ToDataProvider<T>(this IEnumerable<IGrouping<T, T>> group)
            => group.Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() });
    }
}