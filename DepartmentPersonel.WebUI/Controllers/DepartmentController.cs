using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DepartmentPersonel.WebUI.Helper;

namespace DepartmentPersonel.WebUI.Controllers
{
    [LoginFilter]
    [RoutePrefix("departmanlar")]
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        [Route]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString,int? page)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var model = from d in _departmentService.GetAll()
                        select d;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(d => d.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                default:
                    model = model.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }
        
        [Route("yeni-departman")]
        public ActionResult Create()
        {
            return View();
        }

        [Route("yeni-departman")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Insert(department);
                    _departmentService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Değişiklikler kaydedilemedi. Tekrar deneyin ve sorun devam ederse sistem yöneticinize başvurun");
            }
            return View(department);
        }

        [Route("duzenle/{name}-{id}")]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentService.GetById(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [Route("duzenle/{name}-{id}")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Update(department);
                    _departmentService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Değişiklikler kaydedilemedi. Tekrar deneyin ve sorun devam ederse sistem yöneticinize başvurun");
            }
            return View(department);
        }

        [Route("sil/{name}-{id}")]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departmentService.GetById(id);
            if(department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        [Route("sil/{name}-{id}")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _departmentService.Delete(id);
                _departmentService.Save();
            }
            catch (DataException)
            {
                //ModelState.AddModelError("", "Değişiklikler kaydedilemedi. Tekrar deneyin ve sorun devam ederse sistem yöneticinize başvurun");
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}