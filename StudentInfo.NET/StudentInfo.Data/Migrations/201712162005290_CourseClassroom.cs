namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseClassroom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SemesterCourses", "Classroom_Id", "dbo.Classrooms");
            DropIndex("dbo.SemesterCourses", new[] { "Classroom_Id" });
            CreateTable(
                "dbo.ClassroomCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Classroom_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.Classroom_Id)
                .Index(t => t.Classroom_Id);
            
            AddColumn("dbo.SemesterCourses", "ClassroomCourse_Id", c => c.Guid());
            CreateIndex("dbo.SemesterCourses", "ClassroomCourse_Id");
            AddForeignKey("dbo.SemesterCourses", "ClassroomCourse_Id", "dbo.ClassroomCourses", "Id");
            DropColumn("dbo.SemesterCourses", "Classroom_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SemesterCourses", "Classroom_Id", c => c.Guid());
            DropForeignKey("dbo.SemesterCourses", "ClassroomCourse_Id", "dbo.ClassroomCourses");
            DropForeignKey("dbo.ClassroomCourses", "Classroom_Id", "dbo.Classrooms");
            DropIndex("dbo.ClassroomCourses", new[] { "Classroom_Id" });
            DropIndex("dbo.SemesterCourses", new[] { "ClassroomCourse_Id" });
            DropColumn("dbo.SemesterCourses", "ClassroomCourse_Id");
            DropTable("dbo.ClassroomCourses");
            CreateIndex("dbo.SemesterCourses", "Classroom_Id");
            AddForeignKey("dbo.SemesterCourses", "Classroom_Id", "dbo.Classrooms", "Id");
        }
    }
}
