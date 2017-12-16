namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SemesterCourseState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterCourses", "Open", c => c.Boolean(nullable: false));
            DropColumn("dbo.SemesterCourses", "Cost");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SemesterCourses", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.SemesterCourses", "Open");
        }
    }
}
