namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentItemDays : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentItems", "NumberOfDays", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentItems", "NumberOfDays");
        }
    }
}
