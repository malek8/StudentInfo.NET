namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Student_StudentCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourses", "StudentId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourses", "StudentId");
        }
    }
}
