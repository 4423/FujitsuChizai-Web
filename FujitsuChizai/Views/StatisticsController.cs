using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FujitsuChizai.Models.Entities;

namespace FujitsuChizai.Views
{
    public class StatisticsController : Controller
    {
        private ModelContext db = new ModelContext();

        // GET: Statistics
        public ActionResult Index()
        {
            var histories = db.Histories.Include(h => h.Dest).Include(h => h.Origin).Include(h => h.User);
            return View(histories.ToList());
        }

        // GET: Statistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // GET: Statistics/Create
        public ActionResult Create()
        {
            ViewBag.DestId = new SelectList(db.PlaceMarks, "Id", "Name");
            ViewBag.OriginId = new SelectList(db.PlaceMarks, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Id");
            return View();
        }

        // POST: Statistics/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Timestamp,UserId,OriginId,DestId")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DestId = new SelectList(db.PlaceMarks, "Id", "Name", history.DestId);
            ViewBag.OriginId = new SelectList(db.PlaceMarks, "Id", "Name", history.OriginId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Id", history.UserId);
            return View(history);
        }

        // GET: Statistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestId = new SelectList(db.PlaceMarks, "Id", "Name", history.DestId);
            ViewBag.OriginId = new SelectList(db.PlaceMarks, "Id", "Name", history.OriginId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Id", history.UserId);
            return View(history);
        }

        // POST: Statistics/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、http://go.microsoft.com/fwlink/?LinkId=317598 を参照してください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Timestamp,UserId,OriginId,DestId")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DestId = new SelectList(db.PlaceMarks, "Id", "Name", history.DestId);
            ViewBag.OriginId = new SelectList(db.PlaceMarks, "Id", "Name", history.OriginId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Id", history.UserId);
            return View(history);
        }

        // GET: Statistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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
