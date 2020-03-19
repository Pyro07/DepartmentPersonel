using DepartmentPersonel.DataAccess.Concrete.EntityFramework;
using DepartmentPersonel.Entities.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DepartmentPersonel.Business.IdentityManager
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(DepartmentPersonelContext context) : base(context)
        {

        }
    }
}
