namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableupazillaIdinusertable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "UpazillaId", "dbo.Upazilla");
            DropIndex("dbo.User", new[] { "UpazillaId" });
            AlterColumn("dbo.User", "UpazillaId", c => c.Int());
            CreateIndex("dbo.User", "UpazillaId");
            AddForeignKey("dbo.User", "UpazillaId", "dbo.Upazilla", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "UpazillaId", "dbo.Upazilla");
            DropIndex("dbo.User", new[] { "UpazillaId" });
            AlterColumn("dbo.User", "UpazillaId", c => c.Int(nullable: false));
            CreateIndex("dbo.User", "UpazillaId");
            AddForeignKey("dbo.User", "UpazillaId", "dbo.Upazilla", "Id", cascadeDelete: true);
        }
    }
}
