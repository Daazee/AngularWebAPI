namespace AngularWebAPI.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeeImageTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeImage", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.EmployeeImage", new[] { "EmployeeId" });
            DropTable("dbo.EmployeeImage");
        }
    }
}
