using System;
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
using WhoLends.Web.DAL.Implementations;
using WhoLends.Web.Helpers;

namespace WhoLends.Controllers
{
    [Authorize]
    public partial class LendsController : Controller
    {
        private ILendRepository _lendRepository;
        private ILendItemRepository _lendItemRepository;
        private IUserRepository _userRepository;
        private IFileRepository _fileRepository;
        private ILendReturnRepository _lendReturnRepository;

        public LendsController()
        {
            _lendRepository = new LendRepository(new Entities());
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _fileRepository = new FileRepository(new Entities());
            _lendReturnRepository = new LendReturnRepository(new Entities());
        }

        public LendsController(ILendRepository lendrepository, ILendItemRepository lenditemrepository, IUserRepository userrepository, IFileRepository fileRepository, ILendReturnRepository lendreturnrepository)
        {
            _lendRepository = lendrepository;
            _lendItemRepository = lenditemrepository;
            _userRepository = userrepository;
            _fileRepository = fileRepository;
            _lendReturnRepository = lendreturnrepository;
        }

        // GET: Lends
        public virtual ActionResult Index()
        {
            var lendlist = LendList();
            return View(lendlist);

            //best practive way
            //
            //IEnumerable<Lend> _lends = _lendRepository.GetLends();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<Lend, LendViewModel>();
            //    cfg.CreateMap<LendItem, LendItemViewModel>();
            //    cfg.CreateMap<LendReturn, LendReturnViewModel>();
            //});

            //IEnumerable<LendViewModel> viewModelList = Mapper.Map<IEnumerable<Lend>, IEnumerable<LendViewModel>>(_lends);
            //return View(viewModelList.ToList());
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int Id)
        {
            var model = _lendRepository.GetLendByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }
            
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
                cfg.CreateMap<LendItem, LendItemViewModel>();
                cfg.CreateMap<File, FileViewModel>();
                cfg.CreateMap<LendReturn, LendReturnViewModel>();
            });

            LendViewModel vm = Mapper.Map<Lend, LendViewModel>(model);
            LendItemViewModel ItemVM = Mapper.Map<LendItem, LendItemViewModel>(model.LendItem);
            //todo create reference on Return object (model)ö
            LendReturnViewModel lrVM = Mapper.Map<LendReturn, LendReturnViewModel>(model.LendReturn);
            //lrVM.CurrentUserwithID = _userRepository.GetUserById(lrVM.UserId).UserName + " (" + lrVM.CreatedBy.Id + ")";

            ItemVM.CreatedBy = model.LendItem.User;
            ItemVM.CurrentUserwithID = ItemVM.CreatedBy.UserName + " (" + ItemVM.CreatedBy.Id + ")";

            #region images to VMs

            //improve - FIleID to VM - _getFIleById
            //get images of LendItem
            var lenditemImages = _fileRepository.GetFilesByLendItemId(ItemVM.Id);
            List<FileViewModel> listimages = new List<FileViewModel>();

            foreach (var item in lenditemImages)
            {
                FileViewModel vmfile = Mapper.Map<File, FileViewModel>(item);
                listimages.Add(vmfile);
            }

            //add images to Item VM
            ItemVM.ItemImageViewModels = listimages.AsEnumerable();


            //improve - FIleID to VM - _getFIleById
            //get images of LendReturn
            var lendreturnImages = _fileRepository.GetFilesByLendItemId(lrVM.Id);
            List<FileViewModel> lrimageslist = new List<FileViewModel>();

            foreach (var item in lendreturnImages)
            {
                FileViewModel vmfile = Mapper.Map<File, FileViewModel>(item);
                lrimageslist.Add(vmfile);
            }

            //add images to Return VM
            lrVM.ReturnImageViewModels = lrimageslist.AsEnumerable();
            
            #endregion
            
            //stick all to Lend Object
            vm.CreatedBy = model.User;
            vm.CurrentUserwithID = model.User.UserName + " (" + model.User.Id + ")";
            vm.SelectedLendUser = model.LendUser;
            vm.SelectedLendItem = ItemVM;
            vm.LendReturn = lrVM;

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

            var lenditems = Mapper.Map<IEnumerable<LendItemViewModel>>(_lendItemRepository.GetLendItems().OrderBy(d=>d.Id)).ToList().AsEnumerable();
            var alllends = _lendRepository.GetLends();

            var lendsItemKeyCount = alllends.Select(f => new { f.Id, liId = f.LendItemId });
            
            var sortedItemList = new List<LendItemViewModel>();

            //collect the available Items according their quantity
            if (lendsItemKeyCount.Any())
            {
                foreach (var item in lenditems)
                {
                    var usedItemCounter = lendsItemKeyCount.Count(d => d.liId.Equals(item.Id));
                    
                    if (usedItemCounter < item.Quantity)
                    {
                        sortedItemList.Add(item);
                    }
                }
            }
            else
            {
                //adding all items to the list
                sortedItemList = lenditems.ToList();
            }
        
            var viewmodel = new LendViewModel()
            {
                From = DateTime.Now,
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
                var dbUser = General.GetCurrentUser(_userRepository);

                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Lend, LendViewModel>().ReverseMap();
                    cfg.CreateMap<LendReturn, LendReturnViewModel>().ReverseMap();
                });

                var model = Mapper.Map<LendViewModel, Lend>(lendVM);
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
                cfg.CreateMap<LendReturn, LendReturnViewModel>().ReverseMap();
            });

            LendViewModel vm = Mapper.Map<Lend, LendViewModel>(model);
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendViewModel viewModel)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>().ReverseMap();
                cfg.CreateMap<LendReturn, LendReturnViewModel>().ReverseMap();
            });

            var model = Mapper.Map<LendViewModel, Lend>(viewModel);
            _lendRepository.UpdateLend(model);
            _lendRepository.Save();

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
                cfg.CreateMap<LendReturn, LendReturnViewModel>();
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
        
        private List<LendViewModel> LendList()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Lend, LendViewModel>();
                cfg.CreateMap<LendItem, LendItemViewModel>();
                cfg.CreateMap<LendReturn, LendReturnViewModel>();
            });
            
            List<LendViewModel> lItems = new List<LendViewModel>();
            foreach (var l in _lendRepository.GetLends())
            {
                var LImodel = _lendItemRepository.GetLendItemByID(l.LendItemId);
                LendItemViewModel liVM = Mapper.Map<LendItem, LendItemViewModel>(LImodel);

                LendReturnViewModel lrVM = null;
                //if (l.LRId != null)
                //{
                //    //todo exception
                //    //var LRmodel = _lendReturnRepository.GetReturnById(l.LRId.Value);
                //    //lrVM = Mapper.Map<LendReturn, LendReturnViewModel>(LRmodel);
                //}

                var item = new LendViewModel();
                item.Id = l.Id;
                item.From = l.From;
                item.To = l.To;
                item.CreatedAt = l.CreatedAt;
                item.UserId = l.UserId;
                item.LendItemId = l.LendItemId;
                item.LenderUserId = l.LendUser.Id;
                item.SelectedLendUser = _userRepository.GetUserById(item.LenderUserId);
                item.SelectedLendItem = liVM;
                item.LendReturn = lrVM;
                item.CreatedBy = _userRepository.GetUserById(item.UserId);

                lItems.Add(item);
            }

            return lItems;
        }
     }
}
