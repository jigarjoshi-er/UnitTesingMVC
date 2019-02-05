using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UnitTestDemo.Models;

namespace UnitTestDemo.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public UsersController()
        {
        }

        public UsersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? new ApplicationRoleManager(new RoleStore<IdentityRole>());
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            try
            {
                ViewBag.Roles = RoleManager.Roles.AsEnumerable();
                var users = UserManager.Users.ToList();
                var userList = new List<UserList>();

                users.ForEach(x => userList.Add(new UserList(x)));

                return View(userList);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetUserRoles(string Id)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(Id);
                var roles = await UserManager.GetRolesAsync(Id);
                return Json(new { user.Id, user.UserName, Roles = roles }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveUserRoles(UserRolesViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userRoles = await UserManager.GetRolesAsync(model.Id);
                    await UserManager.RemoveFromRolesAsync(model.Id, userRoles.ToArray());
                    await UserManager.AddToRolesAsync(model.Id, model.Roles.ToArray());

                    return Json(1);
                }
                return Json(0);
            }
            catch (Exception)
            {
                return Json(null, JsonRequestBehavior.DenyGet);
            }
        }
    }
}