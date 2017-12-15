namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Date");
        }
    }
}
