using DepartmentPersonel.DataAccess.Concrete.EntityFramework;
using DepartmentPersonel.Entities.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepartmentPersonel.Business.IdentityManager
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole>
    {
        public ApplicationRoleStore(DepartmentPersonelContext context) : base(context)
        {

        }
    }
}
