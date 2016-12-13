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
        private IMapper _mapper;

        public LendsController()
        {
            _lendRepository = new LendRepository(new Entities());
            _lendItemRepository = new LendItemRepository(new Entities());
            _userRepository = new UserRepository(new Entities());
            _fileRepository = new FileRepository(new Entities());
            new LendReturnRepository(new Entities());
            
            _mapper = AutoMapperConfig._mapperConfiguration.CreateMapper();
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

                if (lrVM.FileId != null)
                {
                    File lendreturnImages = _fileRepository.GetFileById(lrVM.FileId.Value);
                    List<FileViewModel> lrimageslist = new List<FileViewModel>();
                
                    FileViewModel lrFileVM = _mapper.Map<File, FileViewModel>(lendreturnImages);
                    lrimageslist.Add(lrFileVM);

                    //add values to Return VM
                    lrVM.ReturnImageViewModels = lrimageslist.AsEnumerable();
                }
                lrVM.CreatedBy = model.LendReturn.User;
            }

           //get images of LendItem
            if (ItemVM.FileId != null)
            {
                File lenditemImages = _fileRepository.GetFileById(ItemVM.FileId.Value);
                List<FileViewModel> listimages = new List<FileViewModel>();
                FileViewModel itemFileVM = _mapper.Map<File, FileViewModel>(lenditemImages);
                listimages.Add(itemFileVM);
       
                //add values to ItemVM
                ItemVM.ItemImageViewModels = listimages.AsEnumerable();
            }
            ItemVM.CreatedBy = model.LendItem.User;

            //stick all to LendVM
            vm.CreatedBy = model.User;
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
            ApplicationUser auser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var dbUser = _userRepository.GetUserByEmail(auser.Email);
            
            //check lenditems quanitty / availability
            var lenditems = _mapper.Map<IEnumerable<LendItemViewModel>>(_lendItemRepository.GetLendItems().OrderBy(d=>d.Id)).ToList();
            var sortedItemList = new List<LendItemViewModel>();

            foreach (var item in lenditems)
            {
                if (item.Avialable != 0 && item.Avialable <= item.Quantity)
                {
                    sortedItemList.Add(item);
                }
            }
        
            var viewmodel = new LendViewModel()
            {
                From = DateTime.Now,
                LendItemsList = sortedItemList,
                UserList = _userRepository.GetUsers()
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

                //update Availability of LendItem
                var lenditem = _lendItemRepository.GetLendItemByID(lendVM.LendItemId);
                lenditem.Avialable --;
                _lendItemRepository.UpdateLendItem(lenditem);
                
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
                //update availablity of LendItem
                var model =_lendRepository.GetLendByID(Id);
                var lenditemmodel = _lendItemRepository.GetLendItemByID(model.LendItemId);
                lenditemmodel.Avialable ++;
                _lendItemRepository.UpdateLendItem(lenditemmodel);

                //Delete Lend
                _lendRepository.DeleteLend(Id);
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
