namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherCourse : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StudentCourses", name: "CourseSemester_Id", newName: "SemesterCourse_Id");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_CourseSemester_Id", newName: "IX_SemesterCourse_Id");
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        SemesterCourse_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SemesterCourses", t => t.SemesterCourse_Id)
                .Index(t => t.SemesterCourse_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "SemesterCourse_Id", "dbo.SemesterCourses");
            DropIndex("dbo.TeacherCourses", new[] { "SemesterCourse_Id" });
            DropTable("dbo.TeacherCourses");
            RenameIndex(table: "dbo.StudentCourses", name: "IX_SemesterCourse_Id", newName: "IX_CourseSemester_Id");
            RenameColumn(table: "dbo.StudentCourses", name: "SemesterCourse_Id", newName: "CourseSemester_Id");
        }
    }
}
