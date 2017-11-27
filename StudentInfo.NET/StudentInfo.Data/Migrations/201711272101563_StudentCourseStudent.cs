namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseStudent : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.StudentCourses", "StudentId");
            AddForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
        }
    }
}
