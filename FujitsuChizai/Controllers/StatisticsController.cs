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


        private List<PlaceMarkFreq> GetOrigin(IEnumerable<History> src)
            => src.GroupBy(x => x.Origin).ToPlaceMarkFreqDesc().ToList();

        private List<PlaceMarkFreq> GetOrigin(IEnumerable<History> src, int floor)
            => src.Where(x => x.Origin.Floor == floor).GroupBy(x => x.Origin).ToPlaceMarkFreqDesc().ToList();

        private List<PlaceMarkFreq> GetDest(IEnumerable<History> src)
           => src.GroupBy(x => x.Dest).ToPlaceMarkFreqDesc().ToList();

        private List<PlaceMarkFreq> GetDest(IEnumerable<History> src, int floor)
            => src.Where(x => x.Dest.Floor == floor).GroupBy(x => x.Dest).ToPlaceMarkFreqDesc().ToList();

        private List<DataProvider> GetCountry(IEnumerable<History> src)
            => src.Select(x => x.User.Country).GroupBy(x => x).ToDataProvider().ToList();

        private List<DataProvider> GetSex(IEnumerable<History> src)
            => src.Select(x => x.User.Sex).GroupBy(x => x).ToDataProvider().ToList();

        private List<DataProvider> GetAge(IEnumerable<History> src)
        {
            var data = src.Select(x => DateTime.Now.Year - x.User.BornIn).GroupBy(x => x).ToDataProvider();
            return AgeHist(data).ToList();
        }

        private IEnumerable<DataProvider> AgeHist(IEnumerable<DataProvider> data)
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
            return hist;
        }


        // GET: Statistics
        public ActionResult Index()
        {
            return View(new StatisticsViewModel()
            {
                Maps = db.Maps.ToList(),
                HasNoContent = true
            });
        }

        
        [HttpPost]
        public ActionResult Index(StatisticsBindingModel input)
        {
            var vm = new StatisticsViewModel()
            {
                Maps = db.Maps.ToList(),
                RequestedMap = db.Maps.Find(input.MapId),
                RequestAllFloors = input.MapId == -1,
            };
                      
            if (vm.RequestedMap == null && !vm.RequestAllFloors)
            {
                vm.HasNoContent = true;
                return View(vm);
            }

            var q = db.Histories
                .Where(x => input.DateBegin <= x.Timestamp && x.Timestamp <= input.DateEnd).ToList()
                .Where(x => input.TimeBegin <= x.Timestamp.TimeOfDay && x.Timestamp.TimeOfDay <= input.TimeEnd);

            if (vm.RequestAllFloors)
            {
                vm.Data = new StatisticsData()
                {
                    Origin = GetOrigin(q),
                    Dest = GetDest(q),
                    Age = GetAge(q),
                    Sex = GetSex(q),
                    Country = GetCountry(q)
                };
                return View(vm);
            }
            else
            {
                int floor = vm.RequestedMap.Floor;
                q = q.Where(x => x.Origin.Floor == floor || x.Dest.Floor == floor);
                vm.Data = new StatisticsData()
                {
                    Origin = GetOrigin(q, floor),
                    Dest = GetDest(q, floor),
                    Age = GetAge(q),
                    Sex = GetSex(q),
                    Country = GetCountry(q)
                };
                return View(vm);
            }
        }


        [ChildActionOnly]
        public ActionResult PartialUser(StatisticsData data)
        {
            return PartialView("_PartialUser", data);
        }

        [ChildActionOnly]
        public ActionResult PartialPlaceTable(List<PlaceMarkFreq> data)
        {
            return PartialView("_PartialPlaceTable", data);
        }

        [ChildActionOnly]
        public ActionResult PartialPlaceMap(List<PlaceMarkFreq> data, Map map)
        {
            return PartialView("_PartialPlaceMap", new MapFreqPair(data, map));
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
