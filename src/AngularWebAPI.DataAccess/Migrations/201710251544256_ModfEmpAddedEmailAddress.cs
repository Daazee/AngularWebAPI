namespace AngularWebAPI.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModfEmpAddedEmailAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmailAddress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmailAddress");
        }
    }
}
