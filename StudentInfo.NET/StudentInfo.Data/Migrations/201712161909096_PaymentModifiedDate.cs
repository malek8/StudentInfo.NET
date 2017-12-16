namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentModifiedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.PaymentItems", "ModifiedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentItems", "ModifiedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Payments", "ModifiedDate");
        }
    }
}
