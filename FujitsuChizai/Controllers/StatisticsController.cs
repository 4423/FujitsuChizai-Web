using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FujitsuChizai.Models.Entities;
using FujitsuChizai.Models;

namespace FujitsuChizai.Controllers
{
    public class StatisticsController : Controller
    {
        private ModelContext db = new ModelContext();

        private StatisticsData GetStatisticsData(int floor, DateTime beginDate, DateTime endDate)
        {
            var q = db.Histories
                .Where(x => x.Origin.Floor == floor || x.Dest.Floor == floor)
                .Where(x => beginDate <= x.Timestamp && x.Timestamp <= endDate);

            if(q.Count() == 0)
            {
                return null;
            }

            return new StatisticsData()
            {
                Origin = q.Where(x => x.Origin.Floor == floor).GroupBy(x => x.Origin).Select(g => new PlaceMarkFreq() { PlaceMark = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList(),
                Dest = q.Where(x => x.Dest.Floor == floor).GroupBy(x => x.Dest).Select(g => new PlaceMarkFreq() { PlaceMark = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList(),
                Age = q.Select(x => DateTime.Now.Year - x.User.BornIn).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() }).ToList(),
                Sex = q.Select(x => x.User.Sex).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() }).ToList(),
                Country = q.Select(x => x.User.Country).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() }).ToList()
            };
        }

        private StatisticsData GetStatisticsData(DateTime beginDate, DateTime endDate)
        {
            var q = db.Histories.Where(x => beginDate <= x.Timestamp && x.Timestamp <= endDate);

            if (q.Count() == 0)
            {
                return null;
            }

            return new StatisticsData()
            {
                Origin = q.GroupBy(x => x.Origin).Select(g => new PlaceMarkFreq() { PlaceMark = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList(),
                Dest = q.GroupBy(x => x.Dest).Select(g => new PlaceMarkFreq() { PlaceMark = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).ToList(),
                Age = AgeHist(q.Select(x => DateTime.Now.Year - x.User.BornIn).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() })),
                Sex = q.Select(x => x.User.Sex).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() }).ToList(),
                Country = q.Select(x => x.User.Country).GroupBy(x => x).Select(g => new DataProvider() { Key = g.Key.ToString(), Value = g.Count() }).ToList()
            };
        }

        private List<DataProvider> AgeHist(IEnumerable<DataProvider> data)
        {
            var hist = new List<DataProvider>()
            {
                new DataProvider() { Key = "0～9" },
                new DataProvider() { Key = "10～19" },
                new DataProvider() { Key = "20～29" },
                new DataProvider() { Key = "30～39" },
                new DataProvider() { Key = "40～49" },
                new DataProvider() { Key = "50～59" },
                new DataProvider() { Key = "60～69" },
                new DataProvider() { Key = "70～79" },
                new DataProvider() { Key = "80～89" },
                new DataProvider() { Key = "90～99" },
                new DataProvider() { Key = "100～109" },
                new DataProvider() { Key = "110～119" }
            }.ToArray();

            foreach (var item in data)
            {
                int age = int.Parse(item.Key);
                if (age / 10 < hist.Length)
                {
                    hist[age / 10].Value += item.Value;
                }
            }
            return hist.ToList();
        }

        // GET: Statistics
        public ActionResult Index()
        {
            return View(new Models.StatisticsViewModel()
            {
                Data = null,
                Maps = db.Maps.ToList(),
                MapId = -2
            });
        }

        [HttpPost]
        public ActionResult Index(int mapId, string daterange)
        {
            var beginDate = DateTime.Parse(daterange.Split('-')[0]);
            var endDate = DateTime.Parse(daterange.Split('-')[1]);
            
            StatisticsData data;
            if (mapId == -1)
            {
                data = GetStatisticsData(beginDate, endDate);
            }
            else
            {
                data = GetStatisticsData(db.Maps.Find(mapId).Floor, beginDate, endDate);
            }

            var a = data.Country.ToJson();

            return View(new Models.StatisticsViewModel()
            {
                Data = data,
                Maps = db.Maps.ToList(),
                MapId = mapId
            });
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
