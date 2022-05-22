namespace HealthService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agedec_to_int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Patient", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patient", "Age", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
