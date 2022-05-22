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
                        entrydate = c.DateTime(nullable: false),
                        registrationdate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        Name = c.String(),
                        Mother = c.String(),
                        Father_or_Husband = c.String(),
                        RelationwithGuardian = c.String(),
                        NID = c.String(),
                        Age = c.Int(nullable: false),
                        Occupation = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        UpazillaId = c.Int(),
                        BankAccount = c.String(),
                        DiseaseId = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disease", t => t.DiseaseId)
                .ForeignKey("dbo.Upazilla", t => t.UpazillaId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
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
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        UpazillaId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Upazilla", t => t.UpazillaId)
                .Index(t => t.UpazillaId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "UpazillaId", "dbo.Upazilla");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.Patient", "UpazillaId", "dbo.Upazilla");
            DropForeignKey("dbo.Patient", "DiseaseId", "dbo.Disease");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "UpazillaId" });
            DropIndex("dbo.Patient", new[] { "DiseaseId" });
            DropIndex("dbo.Patient", new[] { "UpazillaId" });
            DropIndex("dbo.Patient", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Upazilla");
            DropTable("dbo.Patient");
            DropTable("dbo.Disease");
        }
    }
}
