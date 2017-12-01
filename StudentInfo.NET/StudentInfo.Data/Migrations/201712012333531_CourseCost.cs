namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterCourses", "Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.StudentCourses", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourses", "Paid");
            DropColumn("dbo.SemesterCourses", "Cost");
        }
    }
}
