namespace AngularWebAPI.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedEmployeeEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "AppUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "AppUserId", c => c.String(nullable: false));
        }
    }
}
