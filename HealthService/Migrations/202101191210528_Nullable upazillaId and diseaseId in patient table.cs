namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableupazillaIdanddiseaseIdinpatienttable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease");
            DropForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla");
            DropIndex("dbo.Patient", new[] { "UpazillaId" });
            DropIndex("dbo.Patient", new[] { "DiseaseId" });
            AlterColumn("dbo.Patient", "UpazillaId", c => c.Int());
            AlterColumn("dbo.Patient", "DiseaseId", c => c.Int());
            CreateIndex("dbo.Patient", "UpazillaId");
            CreateIndex("dbo.Patient", "DiseaseId");
            AddForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease", "Id");
            AddForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla");
            DropForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease");
            DropIndex("dbo.Patient", new[] { "DiseaseId" });
            DropIndex("dbo.Patient", new[] { "UpazillaId" });
            AlterColumn("dbo.Patient", "DiseaseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Patient", "UpazillaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Patient", "DiseaseId");
            CreateIndex("dbo.Patient", "UpazillaId");
            AddForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease", "Id", cascadeDelete: true);
        }
    }
}
