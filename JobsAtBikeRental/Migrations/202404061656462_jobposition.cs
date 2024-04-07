namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobposition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        ApplicantId = c.Int(nullable: false, identity: true),
                        ApplicantName = c.String(),
                        ApplicantEmail = c.String(),
                        ApplicantPortfolioUrl = c.String(),
                    })
                .PrimaryKey(t => t.ApplicantId);
            
            CreateTable(
                "dbo.JobPositions",
                c => new
                    {
                        JobPositionId = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        JobLocation = c.String(),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobPositions");
            DropTable("dbo.Applicants");
        }
    }
}
