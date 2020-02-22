using DepartmentPersonel.Business.Abstract;
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
    [LoginFilter]
    public class HomeController : Controller
    {
        private IDepartmentService _departmentService;
        private IPersonelService _personelService;
        private IUserService _userService;

        public HomeController(IDepartmentService departmentService, IPersonelService personelService, IUserService userService)
        {
            _departmentService = departmentService;
            _personelService = personelService;
            _userService = userService;
        }
        public ActionResult Index()
        {
            ViewBag.DepartmentsCount = _departmentService.Count();
            ViewBag.PersonelsCount = _personelService.Count();
            ViewBag.UsersCount = _userService.Count();
            return View();
        }
    }
}