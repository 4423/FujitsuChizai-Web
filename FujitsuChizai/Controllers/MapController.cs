using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Controllers
{
    public class MapController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: Map
        public ActionResult Index()
        {
            return View(db.PlaceMarks.ToList());
        }

        // GET: Map/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceMark placeMark = db.PlaceMarks.Find(id);
            if (placeMark == null)
            {
                return HttpNotFound();
            }
            return View(placeMark);
        }

        // GET: Map/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Map/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,X,Y,Floor,Type,Name,LightId")] PlaceMark placeMark)
        {
            if (ModelState.IsValid)
            {
                db.PlaceMarks.Add(placeMark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placeMark);
        }

        // GET: Map/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceMark placeMark = db.PlaceMarks.Find(id);
            if (placeMark == null)
            {
                return HttpNotFound();
            }
            return View(placeMark);
        }

        // POST: Map/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,X,Y,Floor,Type,Name,LightId")] PlaceMark placeMark)
        {
            if (ModelState.IsValid)
            {
                db.Entry(placeMark).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placeMark);
        }

        // GET: Map/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaceMark placeMark = db.PlaceMarks.Find(id);
            if (placeMark == null)
            {
                return HttpNotFound();
            }
            return View(placeMark);
        }

        // POST: Map/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaceMark placeMark = db.PlaceMarks.Find(id);
            db.PlaceMarks.Remove(placeMark);
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
