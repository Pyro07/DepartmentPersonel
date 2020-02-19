namespace DepartmentPersonel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DepartmentPersonel.DataAccess.Concrete.EntityFramework.DepartmentPersonelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DepartmentPersonel.DataAccess.Concrete.EntityFramework.DepartmentPersonelContext";
        }

        protected override void Seed(DepartmentPersonel.DataAccess.Concrete.EntityFramework.DepartmentPersonelContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
