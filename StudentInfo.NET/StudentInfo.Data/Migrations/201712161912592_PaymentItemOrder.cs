namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentItemOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentItems", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentItems", "Order");
        }
    }
}
