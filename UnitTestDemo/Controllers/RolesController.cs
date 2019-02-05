using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace UnitTestDemo.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public RolesController()
        {
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

        // GET: Roles
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(RoleManager.Roles.ToList());
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var result = await RoleManager.CreateAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            return View(model);
        }
    }
}