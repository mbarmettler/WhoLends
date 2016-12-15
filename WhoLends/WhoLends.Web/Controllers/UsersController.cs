using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using WhoLends.Data;
using WhoLends.ViewModels;
using WhoLends.Web.DAL;

namespace WhoLends.Web.Controllers
{
    public partial class UsersController : Controller
    {
        private Entities db = new Entities();

        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UsersController()
        {
            _userRepository = new UserRepository(new Entities());
            new LendReturnRepository(new Entities());

            _mapper = AutoMapperConfig._mapperConfiguration.CreateMapper();
        }

        // GET: Users
        public virtual ActionResult Index()
        {
            IEnumerable<User> _user = _userRepository.GetUsers();

            List<UserViewModel> viewModelList = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_user).ToList();
            return View(viewModelList);
        }

        // GET: Users/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User model = _userRepository.GetUserById(id.Value);
            UserViewModel vm = _mapper.Map<User, UserViewModel>(model);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // GET: Users/Create
        public virtual ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,LockoutEndDateUtc,LockoutEnabled,UserName,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(db.Role, "Id", "Name", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public virtual ActionResult Edit(int id)
        {
            var model = _userRepository.GetUserById(id);
            if (model == null)
            {
                return RedirectToAction(Actions.Index());
            }

            var lenditems = _mapper.Map<IEnumerable<RoleViewModel>>(_userRepository.GetRoles()).ToList();

            UserViewModel vm = _mapper.Map<User, UserViewModel>(model);
            vm.RoleList = lenditems;

            return View(vm);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(UserViewModel userVM)
        {
            //get data from DB
            var model = _userRepository.GetUserById(userVM.Id);

            var updatedUsermodel = _mapper.Map<UserViewModel, User>(userVM);

            //updating values to model
            model.RoleId = updatedUsermodel.RoleId;
            model.Email = updatedUsermodel.Email;
            model.UserName = updatedUsermodel.UserName;

            _userRepository.UpdateUser(model);

            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //update availablity of LendItem
                var model = _userRepository.GetUserById(id);

                //Delete Lend
                _userRepository.DeleteUser(model.Id);
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
