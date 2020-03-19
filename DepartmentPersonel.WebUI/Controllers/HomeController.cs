using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Business.IdentityManager;
using DepartmentPersonel.Entities;
using DepartmentPersonel.WebUI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("anasayfa")]
    public class HomeController : Controller
    {
        private IDepartmentService _departmentService;
        private IPersonelService _personelService;
        private ApplicationUserManager _userManager;

        public HomeController(IDepartmentService departmentService, IPersonelService personelService, ApplicationUserManager userManager)
        {
            _departmentService = departmentService;
            _personelService = personelService;
            _userManager = userManager;
        }

        [Route]
        public ActionResult Index()
        {
            ViewBag.DepartmentsCount = _departmentService.Count();
            ViewBag.PersonelsCount = _personelService.Count();
            ViewBag.UsersCount = _userManager.Users.Count();
            return View();
        }
    }
}