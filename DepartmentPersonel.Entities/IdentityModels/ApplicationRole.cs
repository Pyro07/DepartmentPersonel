using Microsoft.AspNet.Identity.EntityFramework;

namespace DepartmentPersonel.Entities.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {

        }
        public ApplicationRole(string name) : base(name)
        {

        }
    }
}
