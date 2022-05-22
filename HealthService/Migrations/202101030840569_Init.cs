namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disease",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mother = c.String(),
                        Father_or_Husband = c.String(),
                        RelationwithGuardian = c.String(),
                        NID = c.String(),
                        Age = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Occupation = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        UpazillaId = c.Int(nullable: false),
                        BankAccount = c.String(),
                        DiseaseId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disease", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.Upazilla", t => t.UpazillaId, cascadeDelete: true)
                .Index(t => t.UpazillaId)
                .Index(t => t.DiseaseId);
            
            CreateTable(
                "dbo.Upazilla",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla");
            DropForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease");
            DropIndex("dbo.Patient", new[] { "DiseaseId" });
            DropIndex("dbo.Patient", new[] { "UpazillaId" });
            DropTable("dbo.Upazilla");
            DropTable("dbo.Patient");
            DropTable("dbo.Disease");
        }
    }
}
