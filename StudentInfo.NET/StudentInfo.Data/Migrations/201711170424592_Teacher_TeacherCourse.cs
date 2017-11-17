namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teacher_TeacherCourse : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TeacherCourses", "TeacherId");
            AddForeignKey("dbo.TeacherCourses", "TeacherId", "dbo.Teachers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.TeacherCourses", new[] { "TeacherId" });
        }
    }
}
