using System;
using System.Web.Mvc;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;

namespace WhoLends.Web.Controllers
{
    public partial class LendReturnsController : Controller
    {
        private ILendRepository _lendRepository;
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;
        private ILendReturnRepository _lendreturnRepository;

        public LendReturnsController()
        {
            this._lendRepository = new LendRepository(new Entities());
            this._lendItemRepository = new LendItemRepository(new Entities());
            this._userRepository = new UserRepository(new Entities());
            this._lendreturnRepository = new LendReturnRepository(new Entities());
        }

        // GET: LendReturn
        public virtual ActionResult Index()
        {
            return View();
        }


        // GET: LendReturn/Create
        public virtual ActionResult Create(int Id)
        {
            LendReturnViewModel lendReturnVm = new LendReturnViewModel();
            LendViewModel lendVm = new LendViewModel();

            var lend = _lendRepository.GetLendByID(Id);
            //LendViewModel lendVm = Converters.Converter.ConvertToViewModel(lend);

            lendVm.Id = lend.Id;
            lendVm.From = lend.From;
            lendVm.LenderUserId = lend.LenderUserId;
            lendVm.LendItemId = lend.LendItemId;
            lendVm.CreatedAt = lend.CreatedAt;
            lendVm.To = DateTime.Now;

            lendReturnVm.CreatedAt = DateTime.Now;
            lendReturnVm.CreatedBy = Helpers.General.GetCurrentUser(_userRepository);
            lendReturnVm.UserId = lendReturnVm.CreatedBy.Id;
            lendReturnVm.CurrentUserwithID = lendReturnVm.CreatedBy.UserName + " (" + lendReturnVm.UserId + ")";

            lendReturnVm.LendId = lend.Id;

            return View(lendReturnVm);
        }

        // POST: LendReturn/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(LendReturnViewModel lendReturnVM)
        {
            if (ModelState.IsValid)
            {
                lendReturnVM.UserId = 2;

                var model = LoadModel(lendReturnVM);

                _lendreturnRepository.InsertReturn(model);
                _lendreturnRepository.Save();

                var lendmodel = _lendRepository.GetLendByID(model.LendId);
                var lendVM = Converters.Converter.ConvertToViewModel(lendmodel);

                //ToDo
                //set lendreturn id to lend and save
                //set lendVM LendReturn to VM and save model
                lendVM.LendLendReturn = Converters.Converter.ConvertToViewModel(_lendRepository.GetLRByID(lendmodel.LRId.Value));
                lendReturnVM.LRId = lendmodel.LRId;
                

                _lendRepository.UpdateLend(lendmodel);
                _lendRepository.Save();

                return RedirectToAction("..\\Lends\\Index");
            }

            return View(lendReturnVM);
        }

        // GET: LendReturn/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LendReturn lendReturn = db.LendReturn.Find(id);
        //    if (lendReturn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lendReturn);
        //}

        //GET: LendReturn/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LendReturn lendReturn = db.LendReturn.Find(id);
        //    if (lendReturn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(db.User, "Id", "Email", lendReturn.UserId);
        //    return View(lendReturn);
        //}

        // POST: LendReturn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,LendId,Description,CreatedAt,UserId")] LendReturn lendReturn)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lendReturn).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserId = new SelectList(db.User, "Id", "Email", lendReturn.UserId);
        //    return View(lendReturn);
        //}

        // GET: LendReturn/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LendReturn lendReturn = db.LendReturn.Find(id);
        //    if (lendReturn == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lendReturn);
        //}

        // POST: LendReturn/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LendReturn lendReturn = db.LendReturn.Find(id);
        //    db.LendReturn.Remove(lendReturn);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private Data.LendReturn LoadModel(LendReturnViewModel viewModel)
        {
            var model = new Data.LendReturn();

            model.Description = viewModel.Description;
            model.CreatedAt = viewModel.CreatedAt;
            model.UserId = viewModel.UserId;
            model.LendId = viewModel.LendId;

            return model;
        }
    }
}
