﻿using System;
using System.Web.Mvc;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;
using WhoLends.Data;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace WhoLends.Controllers
{
    [Authorize]
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

        public LendsController(ILendRepository lendrepository, ILendItemRepository lenditemrepository, IUserRepository userrepository)
        {
            this._lendRepository = lendrepository;
            this._lendItemRepository = lenditemrepository;
            this._userRepository = userrepository;
        }

        // GET: Lends
        public virtual ActionResult Index()
        {
            var lendlist = LendList();
            return View(lendlist);
            
            //best practive way
            //
            //IEnumerable<Data.Lend> _lends = _lendRepository.GetLends();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Data.Lend, LendViewModel>();
            //    cfg.CreateMap<Data.LendItem, LendItemViewModel>();
            //    cfg.CreateMap<Data.LendReturn, LendReturnViewModel>();
            //});

            //IEnumerable<LendViewModel> viewModelList = Mapper.Map<IEnumerable<Data.Lend>, IEnumerable<LendViewModel>>(_lends);
            //return View(viewModelList);
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int Id)
        {
            //todo - get more data of lend
            // lenditem
            // lenditem.condition
            // quantity (improve model Lend)

            var model = _lendRepository.GetLendByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Data.Lend, LendViewModel>();
                cfg.CreateMap<LendItem, LendItemViewModel>();
            });

            LendViewModel vm = Mapper.Map<Data.Lend, LendViewModel>(model);
            LendItemViewModel ItemVM = Mapper.Map<LendItem, LendItemViewModel>(model.LendItem);
            vm.CreatedBy = model.User;
            vm.SelectedLendUser = model.LendUser;
            vm.SelectedLendItem = ItemVM;

            return View(vm);
        }

        // GET: Lends/Create
        public virtual ActionResult Create()
        {
            ApplicationUser Auser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(Auser.Email);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LendItem, LendItemViewModel>();
            });

            //check lenditems quanitty / availability

            var lenditems = Mapper.Map<IEnumerable<LendItemViewModel>>(_lendItemRepository.GetLendItems()).ToList().AsEnumerable();
            
            var lendsItemKeyCount = _lendRepository.GetLends()
                            .GroupBy(f => f.LendItemId)
                            .Select(g => new { g.Key, Count = g.Count() });

            var sortedItemList = new List<LendItemViewModel>();

            foreach (var item in lendsItemKeyCount)
            {
                var itemId = item.Key;
                var itemCnt = item.Count;
                foreach (var li in lenditems)
                {
                    if (itemId == li.Id && itemCnt < li.Quantity)
                    {
                        sortedItemList.Add(li);
                    }
                }
            }

            var viewmodel = new LendViewModel()
            {
                //todo
                

                LendItemsList = sortedItemList,
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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
            });

            LendViewModel vm = Mapper.Map<Lend, LendViewModel>(model);
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendViewModel viewModel)
        {
            var model = LoadModel(viewModel);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
            });

            LendViewModel vm = Mapper.Map<Lend, LendViewModel>(model);

            return View(vm);
        }

        // Lends/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            var model = _lendRepository.GetLendByID(id.Value);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
            });

            LendViewModel vm = Mapper.Map<Lend, LendViewModel>(model);
            return View(vm);
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

        private List<LendViewModel> LendList()
        {
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
                item.SelectedLendUser = _userRepository.GetUserById(item.LenderUserId);
                item.SelectedLendItem = new LendItemViewModel();
                item.CreatedBy = _userRepository.GetUserById(item.UserId);

                lItems.Add(item);
            }

            return lItems;
        }
     }
}
