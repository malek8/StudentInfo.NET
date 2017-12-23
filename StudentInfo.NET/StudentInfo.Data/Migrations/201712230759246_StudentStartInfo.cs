namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentStartInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "StartTerm", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "StartYear", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "StartYear");
            DropColumn("dbo.Students", "StartTerm");
        }
    }
}
