namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeachToSemesterCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SemesterCourses", "Teacher_Id", c => c.Guid());
            CreateIndex("dbo.SemesterCourses", "Teacher_Id");
            AddForeignKey("dbo.SemesterCourses", "Teacher_Id", "dbo.Teachers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterCourses", "Teacher_Id", "dbo.Teachers");
            DropIndex("dbo.SemesterCourses", new[] { "Teacher_Id" });
            DropColumn("dbo.SemesterCourses", "Teacher_Id");
        }
    }
}
