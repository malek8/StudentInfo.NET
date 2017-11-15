namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classrooms", "Number", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classrooms", "Number");
        }
    }
}
