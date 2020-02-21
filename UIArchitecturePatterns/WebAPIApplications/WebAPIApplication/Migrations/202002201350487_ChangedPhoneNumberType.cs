namespace WebAPIApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPhoneNumberType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerModels", "MobileNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerModels", "MobileNumber", c => c.Int(nullable: false));
        }
    }
}
