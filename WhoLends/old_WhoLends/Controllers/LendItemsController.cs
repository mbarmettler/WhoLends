using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhoLends.Models;

namespace WhoLends.Controllers
{
    public class LendItemsController : Controller
    {
        private LendItemDBContext db = new LendItemDBContext();

        // GET: LendItems
        public ActionResult Index()
        {
            return View(db.LendItemsList.ToList());
        }

        // GET: LendItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendItem lendItem = db.LendItemsList.Find(id);
            if (lendItem == null)
            {
                return HttpNotFound();
            }
            return View(lendItem);
        }

        // GET: LendItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LendItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Photo,Quanitty,Condition")] LendItem lendItem)
        {
            if (ModelState.IsValid)
            {
                db.LendItemsList.Add(lendItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lendItem);
        }

        // GET: LendItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendItem lendItem = db.LendItemsList.Find(id);
            if (lendItem == null)
            {
                return HttpNotFound();
            }
            return View(lendItem);
        }

        // POST: LendItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Photo,Quanitty,Condition")] LendItem lendItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lendItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lendItem);
        }

        // GET: LendItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendItem lendItem = db.LendItemsList.Find(id);
            if (lendItem == null)
            {
                return HttpNotFound();
            }
            return View(lendItem);
        }

        // POST: LendItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LendItem lendItem = db.LendItemsList.Find(id);
            db.LendItemsList.Remove(lendItem);
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
