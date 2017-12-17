namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassroomSchedules : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassroomCourses", "Classroom_Id", "dbo.Classrooms");
            DropForeignKey("dbo.SemesterCourses", "ClassroomCourse_Id", "dbo.ClassroomCourses");
            DropIndex("dbo.ClassroomCourses", new[] { "Classroom_Id" });
            DropIndex("dbo.SemesterCourses", new[] { "ClassroomCourse_Id" });
            CreateTable(
                "dbo.ClassroomSchedules",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Classroom_Id = c.Guid(),
                        ScheduleItem_Id = c.Guid(),
                        SemesterCourse_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.Classroom_Id)
                .ForeignKey("dbo.ClassroomScheduleItems", t => t.ScheduleItem_Id)
                .ForeignKey("dbo.SemesterCourses", t => t.SemesterCourse_Id)
                .Index(t => t.Classroom_Id)
                .Index(t => t.ScheduleItem_Id)
                .Index(t => t.SemesterCourse_Id);
            
            CreateTable(
                "dbo.ClassroomScheduleItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.SemesterCourses", "ClassroomCourse_Id");
            DropTable("dbo.ClassroomCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClassroomCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TimeSlotFrom = c.DateTime(nullable: false),
                        TimeSlotTo = c.DateTime(nullable: false),
                        Classroom_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SemesterCourses", "ClassroomCourse_Id", c => c.Guid());
            DropForeignKey("dbo.ClassroomSchedules", "SemesterCourse_Id", "dbo.SemesterCourses");
            DropForeignKey("dbo.ClassroomSchedules", "ScheduleItem_Id", "dbo.ClassroomScheduleItems");
            DropForeignKey("dbo.ClassroomSchedules", "Classroom_Id", "dbo.Classrooms");
            DropIndex("dbo.ClassroomSchedules", new[] { "SemesterCourse_Id" });
            DropIndex("dbo.ClassroomSchedules", new[] { "ScheduleItem_Id" });
            DropIndex("dbo.ClassroomSchedules", new[] { "Classroom_Id" });
            DropTable("dbo.ClassroomScheduleItems");
            DropTable("dbo.ClassroomSchedules");
            CreateIndex("dbo.SemesterCourses", "ClassroomCourse_Id");
            CreateIndex("dbo.ClassroomCourses", "Classroom_Id");
            AddForeignKey("dbo.SemesterCourses", "ClassroomCourse_Id", "dbo.ClassroomCourses", "Id");
            AddForeignKey("dbo.ClassroomCourses", "Classroom_Id", "dbo.Classrooms", "Id");
        }
    }
}
