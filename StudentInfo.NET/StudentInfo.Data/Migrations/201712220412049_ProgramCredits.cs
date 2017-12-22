namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramCredits : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "TotalCredits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programs", "TotalCredits");
        }
    }
}
