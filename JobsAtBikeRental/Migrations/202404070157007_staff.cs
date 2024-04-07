namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        StaffName = c.String(),
                        StaffEmail = c.String(),
                        ApplicantHistoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaffId)
                .ForeignKey("dbo.ApplicantHistories", t => t.ApplicantHistoryId, cascadeDelete: true)
                .Index(t => t.ApplicantHistoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Staffs", "ApplicantHistoryId", "dbo.ApplicantHistories");
            DropIndex("dbo.Staffs", new[] { "ApplicantHistoryId" });
            DropTable("dbo.Staffs");
        }
    }
}
