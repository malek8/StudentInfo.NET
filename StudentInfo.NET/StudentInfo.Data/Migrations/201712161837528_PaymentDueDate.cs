namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDueDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "DueDate");
        }
    }
}
