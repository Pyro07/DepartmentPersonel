using DepartmentPersonel.Entities.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DepartmentPersonel.Business.IdentityManager
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store, IdentityFactoryOptions<ApplicationRoleManager> options) : base(store)
        {
            var roles = new string[] { "Admin", "User" };
            foreach (var item in roles)
            {
                if (!this.RoleExists(item))
                {
                    this.Create(new ApplicationRole()
                    {
                        Name = item
                    });
                }
            }
        }


        //public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        //{
        //}

        //public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<DepartmentPersonelContext>()));

        //    var roles = new string[] { "Admin", "User" };
        //    foreach (var item in roles)
        //    {
        //        if (!manager.RoleExists(item))
        //        {
        //            manager.Create(new ApplicationRole()
        //            {
        //                Name = item
        //            });
        //        }
        //    }

        //    return manager;
        //}
    }
}
