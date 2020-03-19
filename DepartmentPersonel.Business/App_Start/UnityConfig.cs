using DepartmentPersonel.Business.Abstract;
using DepartmentPersonel.Business.Concrete;
using DepartmentPersonel.Business.IdentityManager;
using DepartmentPersonel.DataAccess.Abstract;
using DepartmentPersonel.DataAccess.Concrete.EntityFramework;
using DepartmentPersonel.Entities.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Web;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace DepartmentPersonel.Business
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
        });

        public static IUnityContainer Container => container.Value;

        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DepartmentPersonelContext>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationUserManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationRoleManager>(new PerRequestLifetimeManager());
            container.RegisterType<ApplicationSignInManager>(new PerRequestLifetimeManager());
            container.RegisterFactory<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication);

            container.RegisterType<IUserStore<ApplicationUser>,
                UserStore<ApplicationUser>>(new InjectionConstructor(typeof(DepartmentPersonelContext)));
            container.RegisterType<IRoleStore<ApplicationRole, string>,
                RoleStore<ApplicationRole, string, IdentityUserRole>>(new InjectionConstructor(typeof(DepartmentPersonelContext)));
           // container.RegisterType<EmailManager>();

            container.RegisterType<IDepartmentService, DepartmentManager>();
            container.RegisterType<IPersonelService, PersonelManager>();
            container.RegisterType<IDepartmentDal, EfDepartmentDal>();
            container.RegisterType<IPersonelDal, EfPersonelDal>();
        }
    }
}