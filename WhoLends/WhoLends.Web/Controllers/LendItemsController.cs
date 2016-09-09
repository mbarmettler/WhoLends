using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WhoLends.Data;

namespace WhoLends.Controllers
{
    public class LendItemsController : Controller
    {
        private Entities dbc = new Entities();

        // GET: LendItems
        public ActionResult Index()
        {
            return View(dbc.LendItem.ToList());
        }

        // GET: LendItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LendItem lendItem = dbc.LendItem.Find(id);
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
                dbc.LendItem.Add(lendItem);
                dbc.SaveChanges();
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
            LendItem lendItem = dbc.LendItem.Find(id);
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
                dbc.Entry(lendItem).State = EntityState.Modified;
                dbc.SaveChanges();
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
            LendItem lendItem = dbc.LendItem.Find(id);
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
            LendItem lendItem = dbc.LendItem.Find(id);
            dbc.LendItem.Remove(lendItem);
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
