using System;
using System.Web.Mvc;
using WhoLends.ViewModels;
using WhoLends.Web.Converters;
using WhoLends.Web.DAL;
using WhoLends.Data;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;

namespace WhoLends.Controllers
{
    public partial class LendsController : Controller
    {
        private ILendRepository _lendRepository;
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;

        public LendsController()
        {
            this._lendRepository = new LendRepository(new Entities());
            this._lendItemRepository = new LendItemRepository(new Entities());
            this._userRepository = new UserRepository(new Entities());            
        }

        public LendsController(ILendRepository lendrepository)
        {
            this._lendRepository = lendrepository;
        }

        // GET: Lends
        public virtual ActionResult Index()
        {
            var viewModel = new LendViewModel();

            List<LendViewModel> lItems = new List<LendViewModel>();
            foreach (var l in _lendRepository.GetLends())
            {
                var item = new LendViewModel();
                item.Id = l.Id;
                item.From = l.From;
                item.To = l.To;
                item.CreatedAt = l.CreatedAt;
                item.UserId = l.UserId;
                item.LendItemId = l.LendItemId;
                item.LenderUserId = l.LendUser.Id;

                lItems.Add(item);
            }

            viewModel.LendList = lItems.AsEnumerable();

            return View(viewModel);
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int Id)
        {
            var model = _lendRepository.GetLendByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            var viewModel = Converter.ConvertToViewModel(model);

            return View(viewModel);
        }

        // GET: Lends/Create
        public virtual ActionResult Create()
        {
            ApplicationUser Auser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(Auser.Email);

            var viewmodel = new LendViewModel()
            {
                //todo
                //check lenditems quanitty / availability
                LendItemsList = _lendItemRepository.GetLendItems(),
                UserList = _userRepository.GetUsers(),                      
                CurrentUserwithID = dbUser.UserName + " (" + dbUser.Id + ")"
            };

            return View(viewmodel);
        }

        // POST: Lends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public virtual ActionResult Create(LendViewModel lendVM)
        {
            if (ModelState.IsValid)
            {
                lendVM.CreatedAt = DateTime.Now;

                //get currently logged in user               
                var dbUser = Web.Helpers.General.GetCurrentUser(_userRepository);

                var model = LoadModel(lendVM);
                model.UserId = dbUser.Id;

                _lendRepository.InsertLend(model);
                _lendRepository.Save();
                return RedirectToAction("Index");
            }

            return View(lendVM);
        }


        // GET: Lends/Edit/5
        public virtual ActionResult Edit(int Id)
        {
            var model = _lendRepository.GetLendByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            var viewModel = Converter.ConvertToViewModel(model);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendViewModel viewModel)
        {
            var model = LoadModel(viewModel);

            viewModel = Converter.ConvertToViewModel(model);
            return View(viewModel);
        }
             
        // POST: Lends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
        {
            try
            {
                var model = _lendRepository.GetLendByID(Id);
                _lendRepository.DeleteLend(Id);
                _lendRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = Id, saveChangesError = true });
            }
            return RedirectToAction("Index");            
        }

        private Data.Lend LoadModel(LendViewModel viewModel)
        {
            var model = _lendRepository.GetLendByID(viewModel.Id) ?? new Data.Lend();

            model.Id = viewModel.Id;
            model.From = viewModel.From;
            model.To = viewModel.To;
            model.CreatedAt = viewModel.CreatedAt;
            model.LendItemId = viewModel.LendItemId;
            model.UserId = viewModel.UserId;
            model.LenderUserId = viewModel.LenderUserId;

            return model;
        }
    }
}
