namespace StudentInfo.WebClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Up1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
        }
    }
}
