using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DepartmentPersonel.WebUI.Controllers
{
    [RoutePrefix("uyelik")]
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("yeni-uyelik")]
        public ActionResult CreateUser()
        {
            return View();
        }

        [Route("yeni-uyelik")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rolId = 2;
                    user.RoleId = rolId;
                    _userService.Insert(user);
                    _userService.Save();
                    return RedirectToAction("LogOn", "Account");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Değişiklikler kaydedilemedi. Tekrar deneyin ve sorun devam ederse sistem yöneticinizi başvurun");
            }
            return View(user);
        }
        
        [Route("giris")]
        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        [Route("giris")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult LogOn(User user)
        {
            var kullaniciVarMi = _userService.GetAll().Where(x => x.Email == user.Email && x.UserPassword == user.UserPassword).SingleOrDefault();
            if (kullaniciVarMi != null)
            {
                if (kullaniciVarMi.RoleId == 1) 
                {
                    Session["KullaniciEmail"] = kullaniciVarMi.Email;
                    FormsAuthentication.SetAuthCookie(kullaniciVarMi.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                //ViewBag.ErrorMessage = "Yetkisiz kullanıcı!";
                return RedirectToAction("Index", "Main");
            }
            ViewBag.ErrorMessage = "Email adresiniz ya da şifreniz yanlış.";
            return View(user);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Main");
        }
        
    }
}