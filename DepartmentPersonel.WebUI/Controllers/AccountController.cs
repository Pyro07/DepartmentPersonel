using DepartmentPersonel.Business.IdentityManager;
using DepartmentPersonel.Entities.IdentityModels;
using DepartmentPersonel.Entities.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using RazorEngine;
using RazorEngine.Templating;
using System.Web;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DepartmentPersonel.WebUI.Controllers
{
    //[RoutePrefix("uyelik")]
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationSignInManager _signInManager;

        public AccountController(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş doğrulanmadı");
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı adı daha önceden alınmıştır");
                    return View(model);
                }
                var newUser = new ApplicationUser
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.EmailAddress
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    if (_userManager.Users.Count() == 1)
                    {
                        await _userManager.AddToRoleAsync(newUser.Id, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser.Id, "User");
                    }
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser.Id);
                    var callBackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newUser.Id, code = code }, Request.Url.Scheme);
                    var templateEmailModel = new SendEmailViewModel
                    {
                        Name = newUser.Name,
                        LastName = newUser.LastName,
                        UserName = newUser.UserName,
                        EmailAddress = newUser.Email,
                        ActivationCode = callBackUrl
                    };
                    string htmlPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Shared/EmailTemplate.cshtml");
                    var htmlAsString = System.IO.File.ReadAllText(htmlPath);
                    var bodyHtml = Engine.Razor.RunCompile(htmlAsString, "RegisterKey", typeof(SendEmailViewModel), templateEmailModel);
                    await _userManager.SendEmailAsync(newUser.Id, "Hesabınızı onaylayın", bodyHtml);
                }
                else
                {
                    var err = "";
                    foreach (var resultError in result.Errors)
                    {
                        err += resultError + " ";
                    }
                    ModelState.AddModelError("", err);
                    return View(model);
                }
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu " + ex.Message;
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (userId == null || code == null)
                {
                    var message = "Hata oluştu";
                    return View(ViewBag.Message = message);
                }
                if (user.EmailConfirmed)
                {
                    ViewBag.Message = "Bu e-posta adresi daha önceden onaylanmıştır";
                    return View();
                }
                else
                {
                    var result = await _userManager.ConfirmEmailAsync(userId, code);
                    if (result.Succeeded)
                    {
                        ViewBag.Message = "E-posta aktivasyon işlemi başarı ile gerçekleşti";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "E-posta aktivasyon işlemi başarız oldu";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Aktivasyon işlemi başarısız " + ex.Message;
            }
            return View(ViewBag.ErrorMessage = "E-posta adresi onaylanmadı");
        }

        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
                switch (result)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Index", "Home");
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return ViewBag.Message = "Hesap onaylanmadı";
                    case SignInStatus.Failure:
                    default:
                        //ModelState.AddModelError("", "Geçersiz oturum açma işlemi");
                        ViewBag.ErrorMessage = "Kullanıcı adınız ya da parolanız yanlış";
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu " + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //var authManager = HttpContext.GetOwinContext().Authentication;
            //authManager.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Giriş doğrulanmadı";
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if((user == null) ||(!await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    ViewBag.Message = "Bu e-posta adresine kayıtlı bir üyelik bulunamadı ya da e-posta adresi onaylanmamış";
                    return View(model);
                }
                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callBackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = resetPasswordToken }, protocol: Request.Url.Scheme);
                var emailTemplateModel = new SendEmailViewModel
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    EmailAddress = user.Email,
                    ResetPasswordToken = callBackUrl
                };
                string htmlPath = System.Web.HttpContext.Current.Server.MapPath("~/Views/Shared/ForgotPasswordEmailTemplate.cshtml");
                var htmlAsString = System.IO.File.ReadAllText(htmlPath);
                var bodyHtml = Engine.Razor.RunCompile(htmlAsString, "forgotPassKey", typeof(SendEmailViewModel), emailTemplateModel);
                await _userManager.SendEmailAsync(user.Id, "Parola sıfırlama bağlantısı", bodyHtml);
                return RedirectToAction("ForgotPasswordConfirmation");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu " + ex.Message;
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View(ViewBag.ErrorMessage = "Bir hata oluştu") : View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorMessage = "Giriş doğrulanmadı";
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
                var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("ResetPasswordConfirmation");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Bir hata oluştu " + ex.Message;
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}