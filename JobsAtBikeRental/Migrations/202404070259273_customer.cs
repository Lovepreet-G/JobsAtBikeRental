namespace JobsAtBikeRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Int(nullable: false, identity: true),
                        customerName = c.String(),
                        customerAddress = c.String(),
                        customerPhone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.customerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
