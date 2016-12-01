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
using WhoLends.Web;
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
        private IMapper _mapper;

        public LendsController()
        {
            _lendRepository = new LendRepository(new Entities());
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _fileRepository = new FileRepository(new Entities());
            _lendReturnRepository = new LendReturnRepository(new Entities());

            _mapper = AutoMapperConfig._mapperConfiguration.CreateMapper();
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
            IEnumerable<Lend> _lends = _lendRepository.GetLends();

            List<LendViewModel> viewModelList = _mapper.Map<IEnumerable<Lend>, IEnumerable<LendViewModel>>(_lends).ToList();
            return View(viewModelList);
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int Id)
        {
            var model = _lendRepository.GetLendByID(Id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }
            
            LendViewModel vm = _mapper.Map<Lend, LendViewModel>(model);
            LendItemViewModel ItemVM = _mapper.Map<LendItem, LendItemViewModel>(model.LendItem);
            LendReturnViewModel lrVM = new LendReturnViewModel();
            
            //get images of LendReturn
            if (model.LendReturn != null)
            {
                lrVM = _mapper.Map<LendReturn, LendReturnViewModel>(model.LendReturn);

                File lendreturnImages = _fileRepository.GetFileById(lrVM.FileId);
                List<FileViewModel> lrimageslist = new List<FileViewModel>();
                
                FileViewModel lrFileVM = _mapper.Map<File, FileViewModel>(lendreturnImages);
                lrimageslist.Add(lrFileVM);

                //add values to Return VM
                lrVM.ReturnImageViewModels = lrimageslist.AsEnumerable();
                lrVM.CreatedBy = model.LendReturn.User;
                lrVM.CurrentUserwithID = lrVM.CreatedBy.UserName + " (" + lrVM.CreatedBy.Id + ")";
            }

           //get images of LendItem
            File lenditemImages = _fileRepository.GetFileById(ItemVM.FileId);
            List<FileViewModel> listimages = new List<FileViewModel>();
            FileViewModel itemFileVM = _mapper.Map<File, FileViewModel>(lenditemImages);
            listimages.Add(itemFileVM);
       
            //add values to ItemVM
            ItemVM.ItemImageViewModels = listimages.AsEnumerable();
            ItemVM.CreatedBy = model.LendItem.User;
            ItemVM.CurrentUserwithID = ItemVM.CreatedBy.UserName + " (" + ItemVM.CreatedBy.Id + ")";

            //stick all to LendVM
            vm.CreatedBy = model.User;
            vm.CurrentUserwithID = model.User.UserName + " (" + model.User.Id + ")";
            vm.SelectedLendUser = model.LendUser;
            vm.SelectedLendItem = ItemVM;
            if (lrVM.Id != 0)
            {
                vm.LendReturn = lrVM;
            }

            return View(vm);
        }

        // GET: Lends/Create
        public virtual ActionResult Create()
        {
            ApplicationUser Auser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(Auser.Email);

            //check lenditems quanitty / availability
            var lenditems = _mapper.Map<IEnumerable<LendItemViewModel>>(_lendItemRepository.GetLendItems().OrderBy(d=>d.Id)).ToList().AsEnumerable();
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
                var model = _mapper.Map<LendViewModel, Lend>(lendVM);
                model.UserId = dbUser.Id;

                _lendRepository.InsertLend(model);

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

            LendViewModel vm = _mapper.Map<Lend, LendViewModel>(model);
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(LendViewModel viewModel)
        {
            //get data from DB
            var model = _lendRepository.GetLendByID(viewModel.Id);

            var updatedLendmodel = _mapper.Map<LendViewModel, Lend>(viewModel);

            //updating values to model
            model.From = updatedLendmodel.From;
            model.To = updatedLendmodel.To;

            _lendRepository.UpdateLend(model);
            
            return RedirectToAction("Index");
        }

        // Lends/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            var model = _lendRepository.GetLendByID(id.Value);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }
            
            LendViewModel vm = _mapper.Map<Lend, LendViewModel>(model);
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
        
     }
}
