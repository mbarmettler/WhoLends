﻿using AutoMapper;
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
using WhoLends.Web;
using WhoLends.Web.DAL;
using WhoLends.Web.DAL.Implementations;
using WhoLends.Web.Helpers;
using File = WhoLends.Data.File;

namespace WhoLends.Controllers
{
    [Authorize]
    public partial class LendItemsController : Controller
    {
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;
        private IFileRepository _fileRepository;
        private IMapper _mapper;

        public LendItemsController()
        {
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _fileRepository = new FileRepository(new Entities());

            _mapper = AutoMapperConfig._mapperConfiguration.CreateMapper();
        }

        public LendItemsController(ILendItemRepository lendItemrepository)
        {
            _lendItemRepository = lendItemrepository;
        }

        // GET: LendItems
        public virtual ActionResult Index()
        {
            var itemlist = LendItemList();
            return View(itemlist);
        }
        
        // GET: LendItems/Details/5
        public virtual ActionResult Details(int Id)
        {
            var model = _lendItemRepository.GetLendItemByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            LendItemViewModel vm = _mapper.Map<LendItem, LendItemViewModel>(model);
            vm.CreatedBy = model.User;
            vm.CurrentUserwithID = model.User.UserName + " (" + model.User.Id + ")";
            vm.Description = model.Description.Replace("\r\n", ", ");

            //get images
            var lenditemImages = _fileRepository.GetFileById(vm.FileId);
            List<FileViewModel> listimages = new List<FileViewModel>();
            FileViewModel itemFileVM = _mapper.Map<File, FileViewModel>(lenditemImages);
            listimages.Add(itemFileVM);

            //add images to Item VM
            vm.ItemImageViewModels = listimages.AsEnumerable();

            return View(vm);
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
        public virtual ActionResult Create(LendItemViewModel lendItemVM, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                //get currently logged in user            
                var dbUser = General.GetCurrentUser(_userRepository);

                lendItemVM.CreatedAt = DateTime.Now;
                
                var lenditemmodel = _mapper.Map<LendItemViewModel, LendItem>(lendItemVM);
                lenditemmodel.UserId = dbUser.Id;
                lenditemmodel.User = lendItemVM.CreatedBy;

                //process Attached Images
                if (uploadfile != null)
                {
                    lendItemVM.ItemImageViewModels = ImageInsert.InsertImages(uploadfile).AsEnumerable();

                    //update lenditem - file ID (only for one image)
                    var firstOrDefault = lendItemVM.ItemImageViewModels.FirstOrDefault();
                    if (firstOrDefault != null)
                        lenditemmodel.FileId = firstOrDefault.Id;
                }

                _lendItemRepository.InsertLendItem(lenditemmodel);

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

            LendItemViewModel vm = _mapper.Map<LendItem, LendItemViewModel>(model);

            return View(vm);
        }

        // POST: LendItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendItemViewModel lendItemVM, HttpPostedFileBase uploadfile)
        {
            //get data from DB
            var model = _lendItemRepository.GetLendItemByID(lendItemVM.Id);

            //convert vm into Model for comparing / updating values
            var updatedlineitemmodel = _mapper.Map<LendItemViewModel, LendItem>(lendItemVM);

            model.Id = updatedlineitemmodel.Id;
            model.Name = updatedlineitemmodel.Name;
            model.Quantity = updatedlineitemmodel.Quantity;
            model.Description = updatedlineitemmodel.Description;
            model.CustomerId = updatedlineitemmodel.CustomerId;
            model.Condition = updatedlineitemmodel.Condition;
            
            //process Attached Images
            if (uploadfile != null)
            {
                lendItemVM.ItemImageViewModels = ImageInsert.InsertImages(uploadfile).AsEnumerable();

                //update lenditem - file ID (only for one image)
                var firstOrDefault = lendItemVM.ItemImageViewModels.FirstOrDefault();
                if (firstOrDefault != null)
                {
                    lendItemVM.FileId = firstOrDefault.Id;
                }

                model.FileId = lendItemVM.FileId;
            }
            else
            {
                if (updatedlineitemmodel.FileId != 0)
                {
                    model.FileId = updatedlineitemmodel.FileId;
                }
            }
            
            _lendItemRepository.UpdateLendItem(model);
            
            return RedirectToAction("Index");
        }

        // LendItems/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            var model = _lendItemRepository.GetLendItemByID(id.Value);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }
            
            LendItemViewModel vm = _mapper.Map<LendItem, LendItemViewModel>(model);
            vm.CreatedBy = model.User;
            vm.CurrentUserwithID = model.User.UserName + " (" + model.User.Id + ")";
            vm.Description = model.Description.Replace("\r\n", ", ");

            //get images
            var lenditemImages = _fileRepository.GetFileById(vm.FileId);
            List<FileViewModel> listimages = new List<FileViewModel>();
            FileViewModel itemFileVM = _mapper.Map<File, FileViewModel>(lenditemImages);
            listimages.Add(itemFileVM);

            //add images to Item VM
            vm.ItemImageViewModels = listimages.AsEnumerable();
            
            return View(vm);
        }

        // POST: LendItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
        {
            try
            {
                _lendItemRepository.GetLendItemByID(Id);
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

        private List<LendItemViewModel> LendItemList()
        {
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

            return lItems;
        }

    }
}
