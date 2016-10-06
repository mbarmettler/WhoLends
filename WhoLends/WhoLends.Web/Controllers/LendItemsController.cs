using MvcJqGrid;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
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

        public LendItemsController()
        {
            this._lendItemRepository = new LendItemRepository(new Entities());
        }

        public LendItemsController(ILendItemRepository lendItemrepository)
        {
            this._lendItemRepository = lendItemrepository;
        }

        // GET: LendItems
        public virtual ActionResult Index()
        {
            var viewmodel = new LendItemViewModel();
            return View(viewmodel);
        }

        // GET: LendItems/Details/5
        public virtual ActionResult Details(int lendItemid)
        {
            var model = _lendItemRepository.GetLendItemByID(lendItemid);
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
            return View();
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
                lendItemVM.CreatedAt = DateTime.Now;

                var model = LoadModel(lendItemVM);
                _lendItemRepository.InsertLendItem(model);
                _lendItemRepository.Save();                

                return RedirectToAction("Index");
            }

            return View(lendItemVM);
        }
             
        // GET: LendItems/Edit/5
        public virtual ActionResult Edit(int lendItemid)
        {
            var model = _lendItemRepository.GetLendItemByID(lendItemid);
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
        public virtual ActionResult DeleteConfirmed(int lendItemid)
        {
            try
            {
                var model = _lendItemRepository.GetLendItemByID(lendItemid);
                _lendItemRepository.DeleteLendItem(lendItemid);
                _lendItemRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = lendItemid, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        private Data.LendItem LoadModel(LendItemViewModel viewModel)
        {
            var model = _lendItemRepository.GetLendItemByID(viewModel.Id) ?? new Data.LendItem();
            return model;
        }

        public virtual ActionResult List(GridSettings gridSettings)
        {
            var lendItems = _lendItemRepository.GetLendItems();
            int totalItems = lendItems.Count();
            var jsonData = new
            {
                total = totalItems / gridSettings.PageSize + 1,
                page = gridSettings.PageIndex,
                records = totalItems,
                rows = (
                         from c in lendItems
                         select new
                         {
                             id = c.Id,
                             cell = new[]
                             {
                                c.Id.ToString(),
                                c.CustomerId.ToString(),
                                c.Name,
                                c.Description,
                                c.CreatedAt.ToString(),
                                c.UserId.ToString()
                             }
                         }).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
