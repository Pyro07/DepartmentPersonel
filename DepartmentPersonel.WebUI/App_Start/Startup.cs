using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DepartmentPersonel.Entities.IdentityModels;
using DepartmentPersonel.Business.IdentityManager;
using System.Web.Mvc;
using Microsoft.Owin.Security.DataProtection;

[assembly: OwinStartup(typeof(DepartmentPersonel.WebUI.App_Start.Startup))]

namespace DepartmentPersonel.WebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            ApplicationUserManager.DataProtectionProvider = app.GetDataProtectionProvider();
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationUserManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationRoleManager>());
            app.CreatePerOwinContext(() => DependencyResolver.Current.GetService<ApplicationSignInManager>());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider=new CookieAuthenticationProvider
                {
                    OnValidateIdentity=SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>
                    (
                        validateInterval:TimeSpan.FromMinutes(30),
                        regenerateIdentity:(manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}
