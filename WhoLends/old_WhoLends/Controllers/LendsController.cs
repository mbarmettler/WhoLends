﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhoLends.Models;

namespace WhoLends.Controllers
{
    public class LendsController : Controller
    {
        private LendDBContext dbc = new LendDBContext();

        // GET: Lends
        public ActionResult Index()
        {
            return View(dbc.LendsList.ToList());
        }

        // GET: Lends/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.LendsList.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // GET: Lends/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LendObjectName,LendObjectDescription,From,To,Employee")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                dbc.LendsList.Add(lend);
                dbc.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lend);
        }

        public ActionResult CreateLendObject()
        {
            return RedirectToAction("Create","LendItems");
        }

        // GET: Lends/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.LendsList.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // POST: Lends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LendObjectName,LendObjectDescription,From,To,Employee")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                dbc.Entry(lend).State = EntityState.Modified;
                dbc.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lend);
        }

        // GET: Lends/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.LendsList.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // POST: Lends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lend lend = dbc.LendsList.Find(id);
            dbc.LendsList.Remove(lend);
            dbc.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbc.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
