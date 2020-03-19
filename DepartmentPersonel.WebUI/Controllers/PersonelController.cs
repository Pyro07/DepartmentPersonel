using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DepartmentPersonel.WebUI.Helper;

namespace DepartmentPersonel.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("personeller")]
    public class PersonelController : Controller
    {
        private IPersonelService _personelService;
        private IDepartmentService _departmentService;

        public PersonelController(IPersonelService personelService, IDepartmentService departmentService)
        {
            _personelService = personelService;
            _departmentService = departmentService;
        }
        
        [Route]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.LastNameParm = sortOrder == "lastName" ? "lastName_desc" : "lastName";
            ViewBag.DepartmentNameParm = sortOrder == "departmentName" ? "departmentName_desc" : "departmentName";

            var model = from personel in _personelService.GetPersonelWithDepartments()
                        select personel;
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(personel => personel.Name.Contains(searchString) || personel.LastName.Contains(searchString) ||
                personel.Department.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(personel => personel.Name);
                    break;
                case "lastName_desc":
                    model = model.OrderByDescending(personel => personel.LastName);
                    break;
                case "lastName":
                    model = model.OrderBy(personel => personel.LastName);
                    break;
                case "departmentName_desc":
                    model = model.OrderByDescending(personel => personel.Department.Name);
                    break;
                case "departmentName":
                    model = model.OrderBy(personel => personel.Department.Name);
                    break;
                default:
                    model = model.OrderBy(personel => personel.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        [Route("yeni-personel")]
        public ActionResult Create()
        {
            DepartmentDropdownList();
            return View();
        }

        [Route("yeni-personel")]
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

        [Route("personel-duzenle/{id}")]
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

        [Route("personel-duzenle/{id}")]
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

        [Route("personel-sil/{id}")]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel = _personelService.GetById(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        [Route("personel-sil/{id}")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                _personelService.Delete(id);
                _personelService.Save();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("Index");
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