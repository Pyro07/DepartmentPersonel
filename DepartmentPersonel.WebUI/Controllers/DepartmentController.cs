using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View(_departmentService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Insert(department);
                _departmentService.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult Edit(int id)
        {
            Department department = _departmentService.GetById(id);
            if (department == null)
            {
                throw new Exception("Departmant bulunamadı");
            }

            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Update(department);
                _departmentService.Save();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult Delete(int id)
        {
            Department department = _departmentService.GetById(id);
            if (department == null)
            {
                throw new Exception("Silinecek departman bulunamadı");
            }
            try
            {
                _departmentService.Delete(id);
                _departmentService.Save();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw new Exception("Departman silinemedi");
            }
        }
    }
}