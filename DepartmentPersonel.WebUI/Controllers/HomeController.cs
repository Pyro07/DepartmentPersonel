using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IDepartmentService _departmentService;
        private IPersonelService _personelService;

        public HomeController(IDepartmentService departmentService, IPersonelService personelService)
        {
            _departmentService = departmentService;
            _personelService = personelService;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}