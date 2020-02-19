namespace WebAPIApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCustomerAndTransactionModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        MobileNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerModels", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionModels", "CustomerId", "dbo.CustomerModels");
            DropIndex("dbo.TransactionModels", new[] { "CustomerId" });
            DropTable("dbo.TransactionModels");
            DropTable("dbo.CustomerModels");
        }
    }
}
