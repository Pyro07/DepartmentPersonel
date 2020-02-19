using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    [RoutePrefix("departmanlar")]
    public class DepartmentController : Controller
    {
        private IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        
        [Route]
        public ActionResult Index()
        {
            return View(_departmentService.GetAll());
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