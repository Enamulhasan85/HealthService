namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimevarinpatienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "entrydate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Patient", "registrationdate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patient", "registrationdate");
            DropColumn("dbo.Patient", "entrydate");
        }
    }
}
