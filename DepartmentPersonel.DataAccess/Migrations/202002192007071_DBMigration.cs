namespace DepartmentPersonel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personel", "IdentityNumber", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personel", "IdentityNumber", c => c.Long(nullable: false));
        }
    }
}
