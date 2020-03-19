using DepartmentPersonel.Business.IdentityManager;
using DepartmentPersonel.Entities.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DepartmentPersonel.WebUI.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationUserManager _userManager;

        public ManageController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            try
            {
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = _userManager.FindById(id);
                var model = new UserProfilePasswordViewModel
                {
                    UserProfileViewModel=new UserProfileViewModel()
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        Email = user.Email
                    }
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bilgiler getirilirken bir hata oluştu " + ex.Message;
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProfile(UserProfilePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Giriş doğrulanmadı";
                return View("UserProfile", model);
            }
            try
            {
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var user = _userManager.FindById(id);
                user.Name = model.UserProfileViewModel.Name;
                user.LastName = model.UserProfileViewModel.LastName;
                await _userManager.UpdateAsync(user);
                TempData["Message"] = "Bilgiler başarı ile güncellendi";
                return RedirectToAction("UserProfile");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu " + ex.Message;
                return View("UserProfile", model);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserProfilePasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("UserProfile", model);
                }
                var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.ChangePasswordViewModel.OldPassword,
                    model.ChangePasswordViewModel.NewPassword);
                if(result.Succeeded)
                {
                    TempData["Message"] = "Parolanız başarı ile değiştirildi";
                    return RedirectToAction("UserProfile");
                }
                else
                {
                    TempData["ErrorMessage"] = "Parolanız değiştirilemedi";
                    return RedirectToAction("UserProfile");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu " + ex.Message;
                return View("UserProfile", model);
            }
        }
    }
}