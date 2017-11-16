namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourse_Grade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourses", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourses", "Grade");
        }
    }
}
