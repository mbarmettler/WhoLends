using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhoLends.Data;
using WhoLends.ViewModels;
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

        public LendItemsController()
        {
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _fileRepository = new FileRepository(new Entities());
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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LendItem, LendItemViewModel>();
                cfg.CreateMap<File, FileViewModel>();
            });

            LendItemViewModel vm = Mapper.Map<LendItem, LendItemViewModel>(model);


            //get images
            var lenditemImages = _fileRepository.GetFilesByLendItemId(vm.Id);
            List<FileViewModel> listimages = new List<FileViewModel>();
            
            foreach (var item in lenditemImages)
            {
                FileViewModel vmfile = Mapper.Map<File, FileViewModel>(item);
                listimages.Add(vmfile);
            }

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
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Lend, LendViewModel>();
                    cfg.CreateMap<LendItem, LendItemViewModel>().ReverseMap();
                    cfg.CreateMap<File, FileViewModel>().ReverseMap();
                });
                
                //get currently logged in user            
                var dbUser = General.GetCurrentUser(_userRepository);

                lendItemVM.CreatedAt = DateTime.Now;
                
                var lenditemmodel = Mapper.Map<LendItemViewModel, LendItem>(lendItemVM);
                lenditemmodel.UserId = dbUser.Id;
                lenditemmodel.User = lendItemVM.CreatedBy;

                _lendItemRepository.InsertLendItem(lenditemmodel);
                _lendItemRepository.Save();

                //process Attached Images
                if (uploadfile.ContentLength > 0)
                {
                    FileViewModel fileVM = new FileViewModel();
                    using (var reader = new BinaryReader(uploadfile.InputStream))
                    {
                        fileVM.Content = reader.ReadBytes(uploadfile.ContentLength);
                    }

                    fileVM.FileName = uploadfile.FileName;
                    fileVM.LendItemId = lenditemmodel.Id;

                    //add file to DB
                    var filemodel = Mapper.Map<FileViewModel, File>(fileVM);
                    _fileRepository.InsertFile(filemodel);
                    _fileRepository.Save();

                    //adding to LendItem VM
                    List<FileViewModel> list = new List<FileViewModel>();
                    list.Add(fileVM);
                    lendItemVM.ItemImageViewModels = list.AsEnumerable();

                    //update lenditem - file ID
                    lenditemmodel.FileId = filemodel.Id;

                    _lendItemRepository.UpdateLendItem(lenditemmodel);
                    _lendItemRepository.Save();
                }

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

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LendItem, LendItemViewModel>();
            });

            LendItemViewModel vm = Mapper.Map<LendItem, LendItemViewModel>(model);

            return View(vm);
        }

        // POST: LendItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendItemViewModel lendItemVM)
        {
            //todo - does not work yet

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LendItem, LendItemViewModel>().ReverseMap();
            });

            var model = Mapper.Map<LendItemViewModel, LendItem>(lendItemVM);
            //_lendItemRepository.UpdateLendItem(model);
            //_lendItemRepository.Save();

            LendItemViewModel vm = Mapper.Map<LendItem, LendItemViewModel>(model);

            return View(vm);
        }

        // LendItems/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            var model = _lendItemRepository.GetLendItemByID(id.Value);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LendItem, LendItemViewModel>();
            });

            LendItemViewModel vm = Mapper.Map<LendItem, LendItemViewModel>(model);
            return View(vm);
        }

        // POST: LendItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Delete(int Id)
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

        //private File LoadFileModel(FileViewModel viemwModel)
        //{
        //    var model = _fileRepository.GetFileById(viemwModel.Id) ?? new File();

        //    model.Id = viemwModel.Id;
        //    model.LendItemId = viemwModel.LendItemId;
        //    model.Content = viemwModel.Content;
        //    model.FileName = viemwModel.FileName;
        //    model.LendReturnId = viemwModel.LendReturnId;

        //    return model;
        //}

    }
}
