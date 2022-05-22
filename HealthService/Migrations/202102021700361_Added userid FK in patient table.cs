namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddeduseridFKinpatienttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patient", "UserId", c => c.Int());
            CreateIndex("dbo.Patient", "UserId");
            AddForeignKey("dbo.Patient", "UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "UserId", "dbo.User");
            DropIndex("dbo.Patient", new[] { "UserId" });
            DropColumn("dbo.Patient", "UserId");
        }
    }
}
