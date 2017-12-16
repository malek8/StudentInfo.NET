namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentItemModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentItems", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentItems", "ModifiedDate");
        }
    }
}
