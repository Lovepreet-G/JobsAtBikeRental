namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rentalhistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentalHistories",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        BikeId = c.Int(nullable: false),
                        customerId = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        from = c.DateTime(nullable: false),
                        to = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId)
                .ForeignKey("dbo.bikes", t => t.BikeId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .ForeignKey("dbo.Staffs", t => t.StaffId, cascadeDelete: true)
                .Index(t => t.BikeId)
                .Index(t => t.customerId)
                .Index(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RentalHistories", "StaffId", "dbo.Staffs");
            DropForeignKey("dbo.RentalHistories", "customerId", "dbo.Customers");
            DropForeignKey("dbo.RentalHistories", "BikeId", "dbo.bikes");
            DropIndex("dbo.RentalHistories", new[] { "StaffId" });
            DropIndex("dbo.RentalHistories", new[] { "customerId" });
            DropIndex("dbo.RentalHistories", new[] { "BikeId" });
            DropTable("dbo.RentalHistories");
        }
    }
}
