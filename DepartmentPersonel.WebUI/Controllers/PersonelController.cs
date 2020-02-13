using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    public class PersonelController : Controller
    {
        private IPersonelService _personelService;
        private IDepartmentService _departmentService;

        public PersonelController(IPersonelService personelService, IDepartmentService departmentService)
        {
            _personelService = personelService;
            _departmentService = departmentService;
        }
        // GET: Personel
        public ActionResult Index()
        {
            var model = _personelService.GetPersonelWithDepartments();
            return View(model);
        }

        public ActionResult Create()
        {
            DepartmentDropdownList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Personel personel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personelService.Insert(personel);
                    _personelService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                throw new Exception("Personel oluşturulamadı");
            }

            return View(personel);
        }

        public ActionResult Edit(int id)
        {
            DepartmentDropdownList();
            Personel personel = _personelService.GetById(id);
            if (personel == null)
            {
                throw new Exception("Personel bulunamadı");
            }
            return View(personel);
        }

        [HttpPost]
        public ActionResult Edit(Personel personel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _personelService.Update(personel);
                    _personelService.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                throw new Exception("Personel güncellenemedi");
            }
            return View(personel);
        }

        public ActionResult Delete(int id)
        {
            Personel personel = _personelService.GetById(id);
            if (personel == null)
            {
                throw new Exception("Silinecek personel bulunamdı");
            }
            try
            {
                _personelService.Delete(id);
                _personelService.Save();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw new Exception("Personel silinemedi");
            }
        }

        private void DepartmentDropdownList(/*object selectedDepartment = null*/)
        {
            //var departmentQuery = from d in _departmentService.GetAll()
            //                      orderby d.Name
            //                      select d;
            //ViewBag.PersonelDepartments = new SelectList(departmentQuery, "DepartmentId", "Name", selectedDepartment);
            ViewBag.PersonelDepartments = _departmentService.GetAll();
        }
    }
}