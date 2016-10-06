using System;
using System.Net;
using System.Web.Mvc;
using WhoLends.ViewModels;
using WhoLends.Web.Converters;
using WhoLends.Web.DAL;
using WhoLends.Data;
using System.Data;
using MvcJqGrid;
using System.Linq;

namespace WhoLends.Controllers
{
    public partial class LendsController : Controller
    {
        private ILendRepository _lendRepository;

        public LendsController()
        {
            this._lendRepository = new LendRepository(new Entities());
        }

        public LendsController(ILendRepository lendrepository)
        {
            this._lendRepository = lendrepository;
        }

        // GET: Lends
        public virtual ActionResult Index()
        {
            var viewModel = new LendViewModel();
            return View(viewModel);
        }

        // GET: Lends/Details/5
        public virtual ActionResult Details(int lendId)
        {
            var model = _lendRepository.GetLendByID(lendId);
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
            return View();
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
                var model = LoadModel(lendVM);
                _lendRepository.InsertLend(model);
                _lendRepository.Save();
                return RedirectToAction("Index");
            }

            return View(lendVM);
        }


        // GET: Lends/Edit/5
        public virtual ActionResult Edit(int lendId)
        {
            var model = _lendRepository.GetLendByID(lendId);
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
        public virtual ActionResult Delete(int lendId)
        {
            try
            {
                var model = _lendRepository.GetLendByID(lendId);
                _lendRepository.DeleteLend(lendId);
                _lendRepository.Save();
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = lendId, saveChangesError = true });
            }
            return RedirectToAction("Index");            
        }

        private Data.Lend LoadModel(LendViewModel viewModel)
        {
            var model = _lendRepository.GetLendByID(viewModel.Id) ?? new Data.Lend();
            return model;
        }

        public virtual ActionResult List(GridSettings gridSettings)
        {
            var lends = _lendRepository.GetLends();
            int totalitems = lends.Count();
            var jsonData = new
            {
                total = totalitems / gridSettings.PageSize + 1,
                page = gridSettings.PageIndex,
                records = totalitems,
                rows = (
                         from c in lends
                         select new
                         {
                             id = c.Id,
                             cell = new[]
                             {
                                c.Id.ToString(),
                                c.LenderUserId.ToString(),
                                c.From.ToShortDateString(),
                                c.To.ToString(),
                                c.LendItem.Name.ToString(),
                                c.LendItem.CustomerId.ToString(),
                                c.CreatedAt.ToString(),
                                c.UserId.ToString()
                             }
                         }).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}
