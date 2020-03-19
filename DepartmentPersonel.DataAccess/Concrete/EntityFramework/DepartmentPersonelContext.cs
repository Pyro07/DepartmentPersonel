using DepartmentPersonel.Entities;
using DepartmentPersonel.Entities.ApplicationModels;
using DepartmentPersonel.Entities.IdentityModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Concrete.EntityFramework
{
    public class DepartmentPersonelContext : IdentityDbContext<ApplicationUser>
    {
        public DepartmentPersonelContext() : base (nameOrConnectionString: "DepartmentPersonelContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Personel> Personels { get; set; }
    }
}
