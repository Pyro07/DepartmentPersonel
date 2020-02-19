using DepartmentPersonel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentPersonel.DataAccess.Concrete.EntityFramework
{
    public class DepartmentPersonelContext : DbContext
    {
        public DepartmentPersonelContext() : base (nameOrConnectionString: "DepartmentPersonelContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Personel> Personels { get; set; }
    }
}
