using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.Converters;
using WhoLends.Web.DAL;

namespace WhoLends.Controllers
{
    public partial class LendItemsController : Controller
    {
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;

        public LendItemsController()
        {
            this._lendItemRepository = new LendItemRepository(new Entities());
            this._userRepository = new UserRepository(new Entities());
        }

        public LendItemsController(ILendItemRepository lendItemrepository)
        {
            this._lendItemRepository = lendItemrepository;
        }

        // GET: LendItems
        public virtual ActionResult Index()
        {
            var viewmodel = new LendItemViewModel();

            List<LendItemViewModel> lItems = new List<LendItemViewModel>();
            foreach (var l in _lendItemRepository.GetLendItems())
            {
                var item = new LendItemViewModel();
                item.Id = l.Id;
                item.Name = l.Name;
                item.Description = l.Description;
                item.Condition = l.Condition;
                item.CreatedAt = l.CreatedAt;
                item.CustomerId = l.CustomerId;
                item.Quantity = l.Quantity;                
                item.UserId = l.UserId;
                item.CreatedBy = _userRepository.GetUserById(item.UserId);

                lItems.Add(item);
            }

            viewmodel.LendItemList = lItems.AsEnumerable();

            return View(viewmodel);
        }

        // GET: LendItems/Details/5
        public virtual ActionResult Details(int Id)
        {
            var model = _lendItemRepository.GetLendItemByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            var viewModel = Converter.ConvertToViewModel(model);

            return View(viewModel);
        }

        // GET: LendItems/Create
        public virtual ActionResult Create()
        {
            ApplicationUser Auser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(Auser.Email);

            var viewmodel = new LendItemViewModel()
            {
                CurrentUserwithID = dbUser.UserName + " (" + dbUser.Id + ")"
            };
            return View(viewmodel);
        }

        // POST: LendItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(LendItemViewModel lendItemVM)
        {
            if (ModelState.IsValid)
            {
                //get currently logged in user            
                var dbUser = Web.Helpers.General.GetCurrentUser(_userRepository);

                lendItemVM.CreatedAt = DateTime.Now;

                var model = LoadModel(lendItemVM);
                model.UserId = dbUser.Id;
                model.User = lendItemVM.CreatedBy;

                _lendItemRepository.InsertLendItem(model);
                _lendItemRepository.Save();                

                return RedirectToAction("Index");
            }

            return View(lendItemVM);
        }
             
        // GET: LendItems/Edit/5
        public virtual ActionResult Edit(int Id)
        {
            var model = _lendItemRepository.GetLendItemByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            var viewModel = Converter.ConvertToViewModel(model);

            return View(viewModel);
        }

        // POST: LendItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendItemViewModel lendItemVM)
        {
            var model = LoadModel(lendItemVM);

            lendItemVM = Converter.ConvertToViewModel(model);
            return View(lendItemVM);
        }

        // POST: LendItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int Id)
        {
            try
            {
                var model = _lendItemRepository.GetLendItemByID(Id);
                _lendItemRepository.DeleteLendItem(Id);
                _lendItemRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = Id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        private Data.LendItem LoadModel(LendItemViewModel viewModel)
        {
            var model = _lendItemRepository.GetLendItemByID(viewModel.Id) ?? new Data.LendItem();

            model.Id = viewModel.Id;
            model.Description = viewModel.Description;
            model.Name = viewModel.Name;
            model.CreatedAt = viewModel.CreatedAt;
            model.UserId = viewModel.UserId;
            model.Quantity = viewModel.Quantity;
            model.Condition = viewModel.Condition;
            model.CustomerId = viewModel.CustomerId;

            return model;
        }        
    }
}
