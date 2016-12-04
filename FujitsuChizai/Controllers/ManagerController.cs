using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FujitsuChizai.Models.Entities;
using FujitsuChizai.Converters;
using System.IO;
using System.Drawing;
using FujitsuChizai.Models;

namespace FujitsuChizai.Controllers
{   
    public class ManagerController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: Manager
        public ActionResult Index()
        {
            return View(db.Maps.ToList());
        }

        // GET: Manager/Map/5
        public ActionResult Map(int? id)
        {
            var map = db.Maps.Find(id);
            var edges = db.Edges.Where(x => x.PlaceMark1.Floor == map.Floor).ToList();
            return View(new MapViewModel()
            {
                PlaceMarks = db.PlaceMarks.Where(x => x.Floor == map.Floor).ToList(),
                Map = map,
                Paths = edges.Select(e => new MapViewModel.PathData()
                {
                    Edge = e,
                    D = e.ToPathD(),
                    StrokeDash = e.ToStrokeDash()
                }).ToList()
            });
        }

        // GET: Manager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }


        private bool IsMapBindingModelValid(MapBindingModel map)
            => ModelState.IsValid
            && map.Picture != null
            && map.Picture.ContentType.Contains("image")
            && map.Picture.ContentLength > 0;

        private void SaveImage(string fileName, HttpPostedFileBase uploadedPict)
        {
            var destinationFolder = Server.MapPath("~/Resources/Map");
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // ファイル保存
            var path = Path.Combine(destinationFolder, fileName);
            uploadedPict.SaveAs(path);            
        }

        // POST: Manager/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Floor,Description,Picture")] MapBindingModel input)
        {
            if (IsMapBindingModelValid(input))
            {
                var fileName = Path.GetFileName(input.Picture.FileName);
                Image image = null;
                try
                {
                    // 画像化
                    image = Image.FromStream(input.Picture.InputStream);
                }
                catch (ArgumentException)
                {
                    return View();
                }

                SaveImage(fileName, input.Picture);

                // DB保存
                var m = new Map()
                {
                    Floor = input.Floor,
                    Description = input.Description,
                    MapImageFilePath = fileName,
                    Width = image.Width,
                    Height = image.Height
                };
                db.Maps.Add(m);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(input);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(new MapBindingModel() { Description = map.Description, Floor = map.Floor });
        }

        // POST: Manager/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "Floor,Description,Picture")] MapBindingModel input)
        {
            if (IsMapBindingModelValid(input))
            {
                var fileName = Path.GetFileName(input.Picture.FileName);
                var image = SaveImage(fileName, input.Picture);

                // DB保存
                var map = db.Maps.Find(id);
                map.Floor = input.Floor;
                map.Description = input.Description;
                map.MapImageFilePath = fileName;
                map.Width = image.Width;
                map.Height = image.Height;

                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(input);
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Map map = db.Maps.Find(id);
            if (map == null)
            {
                return HttpNotFound();
            }
            return View(map);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Map map = db.Maps.Find(id);
            int f = map.Floor;
            var edges = db.Edges.Where(x => x.PlaceMark1.Floor == f || x.PlaceMark2.Floor == f);
            var pm = db.PlaceMarks.Where(x => x.Floor == f);

            db.Maps.Remove(map);
            db.Edges.RemoveRange(edges);
            db.PlaceMarks.RemoveRange(pm);
            db.SaveChanges();
            return RedirectToAction("Index");
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
