namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bikes",
                c => new
                    {
                        BikeId = c.Int(nullable: false, identity: true),
                        BikeBrand = c.String(),
                        BikeModel = c.String(),
                        BikeRate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BikeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.bikes");
        }
    }
}
