namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicanthistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicantHistories",
                c => new
                    {
                        ApplicantHistoryId = c.Int(nullable: false, identity: true),
                        ApplicantId = c.Int(nullable: false),
                        JobPositionId = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ApplicantHistoryId)
                .ForeignKey("dbo.Applicants", t => t.ApplicantId, cascadeDelete: true)
                .ForeignKey("dbo.JobPositions", t => t.JobPositionId, cascadeDelete: true)
                .Index(t => t.ApplicantId)
                .Index(t => t.JobPositionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicantHistories", "JobPositionId", "dbo.JobPositions");
            DropForeignKey("dbo.ApplicantHistories", "ApplicantId", "dbo.Applicants");
            DropIndex("dbo.ApplicantHistories", new[] { "JobPositionId" });
            DropIndex("dbo.ApplicantHistories", new[] { "ApplicantId" });
            DropTable("dbo.ApplicantHistories");
        }
    }
}
