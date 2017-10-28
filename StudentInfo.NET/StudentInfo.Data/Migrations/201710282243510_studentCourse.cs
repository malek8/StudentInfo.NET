namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        CourseDate = c.DateTime(nullable: false),
                        CourseState = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        Course_Id = c.Guid(),
                        Student_Id = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.UserDetails", t => t.Student_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.StudentCourses", "Student_Id", "dbo.UserDetails");
            DropIndex("dbo.StudentCourses", new[] { "Course_Id" });
            DropTable("dbo.StudentCourses", new[] { "Student_Id" });
        }
    }
}
