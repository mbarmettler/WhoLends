using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;
using WhoLends.Web.Helpers;

namespace WhoLends.Web.Controllers
{
    public partial class LendReturnsController : Controller
    {
        private ILendRepository _lendRepository;
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;
        private ILendReturnRepository _lendreturnRepository;
        private IMapper _mapper;

        public LendReturnsController()
        {
            _lendRepository = new LendRepository(new Entities());
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _lendreturnRepository = new LendReturnRepository(new Entities());

            _mapper = AutoMapperConfig._mapperConfiguration.CreateMapper();
        }

        // GET: LendReturn
        public virtual ActionResult Index()
        {
            return View();
        }

        // GET: LendReturn/Create
        public virtual ActionResult Create(int Id)
        {
            var model = _lendRepository.GetLendByID(Id);

            LendViewModel lendVM = _mapper.Map<Lend, LendViewModel>(model);
            lendVM.To = DateTime.Now;

            LendReturnViewModel lendReturnVm = new LendReturnViewModel
            {
                LendId =  lendVM.Id,
                CreatedAt = DateTime.Now,
                CreatedBy = General.GetCurrentUser(_userRepository)
            };
            lendReturnVm.UserId = lendReturnVm.CreatedBy.Id;
            lendReturnVm.CurrentUserwithID = lendReturnVm.CreatedBy.UserName + " (" + lendReturnVm.UserId + ")";
            
            return View(lendReturnVm);
        }

        // POST: LendReturn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(LendReturnViewModel lendReturnVM, HttpPostedFileBase uploadfile)
        {
            //todo refactoring and improvement§
            if (ModelState.IsValid)
            {
                lendReturnVM.UserId = General.GetCurrentUser(_userRepository).Id;
                lendReturnVM.CreatedBy = General.GetCurrentUser(_userRepository);
                lendReturnVM.CreatedAt = DateTime.Now;

                //process Attached Images
                if (uploadfile != null)
                {
                    lendReturnVM.ReturnImageViewModels = ImageInsert.InsertImages(uploadfile).AsEnumerable();

                    //create Return model
                    var firstOrDefault = lendReturnVM.ReturnImageViewModels.FirstOrDefault();
                    if (firstOrDefault != null)
                        lendReturnVM.FileId = firstOrDefault.Id;
                }

                var model = _mapper.Map<LendReturnViewModel, LendReturn>(lendReturnVM);
                _lendreturnRepository.InsertReturn(model);

                //get Lend and update it
                var lendmodel = _lendRepository.GetLendByID(model.LendId);
                lendmodel.LendReturnId = model.Id;
                lendmodel.To = DateTime.Now;

                _lendRepository.UpdateLend(lendmodel);

                return RedirectToAction("..\\Lends\\Index");
            }

            return View(lendReturnVM);
        }


        //improve - load return-partial view from here
        //[ChildActionOnly]
        //public virtual PartialViewResult GetReturn(int lrId)
        //{
        //    var model = _lendreturnRepository.GetReturnById(lrId);
        //    LendReturnViewModel lrVM = Mapper.Map<LendReturn, LendReturnViewModel>(model);

        //    return PartialView("~/Views/LendReturns/_LendReturnDetail.cshtml", lrVM);
        //}

        #region not used code

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

        #endregion
    }
}
