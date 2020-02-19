namespace DepartmentPersonel.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Personel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdentityNumber = c.Long(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 70),
                        PersonelSalary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personel", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Personel", new[] { "DepartmentId" });
            DropTable("dbo.Personel");
            DropTable("dbo.Department");
        }
    }
}
