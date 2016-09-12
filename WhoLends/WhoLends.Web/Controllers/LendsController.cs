using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhoLends.Data;

namespace WhoLends.Controllers
{
    public partial class LendsController : Controller
    {
        private Entities dbc = new Entities();

        // GET: Lends
        public virtual ActionResult Index()
        {
            return View(dbc.Lend.ToList());
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.Lend.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // GET: Lends/Create
        public virtual ActionResult Create()
        {
            return View();
        }

        // POST: Lends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "ID,LendObjectName,LendObjectDescription,From,To,Employee")] Lend lend)
        {
            if (ModelState.IsValid)
            {
                dbc.Lend.Add(lend);
                dbc.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lend);
        }

        public virtual ActionResult CreateLendObject()
        {
            return RedirectToAction("Create", "LendItems");
        }

        // GET: Lends/Edit/5
        public virtual ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.Lend.Find(id);
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
        public virtual ActionResult Edit([Bind(Include = "ID,LendObjectName,LendObjectDescription,From,To,Employee")] Lend lend)
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
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lend lend = dbc.Lend.Find(id);
            if (lend == null)
            {
                return HttpNotFound();
            }
            return View(lend);
        }

        // POST: Lends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            Lend lend = dbc.Lend.Find(id);
            dbc.Lend.Remove(lend);
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
