namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassroomSchedulesRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClassroomSchedules", "ScheduleItem_Id", "dbo.ClassroomScheduleItems");
            DropForeignKey("dbo.ClassroomSchedules", "SemesterCourse_Id", "dbo.SemesterCourses");
            DropIndex("dbo.ClassroomSchedules", new[] { "ScheduleItem_Id" });
            DropIndex("dbo.ClassroomSchedules", new[] { "SemesterCourse_Id" });
            AddColumn("dbo.ClassroomScheduleItems", "ClassroomSchedule_Id", c => c.Guid());
            AddColumn("dbo.SemesterCourses", "Schedule_Id", c => c.Guid());
            CreateIndex("dbo.ClassroomScheduleItems", "ClassroomSchedule_Id");
            CreateIndex("dbo.SemesterCourses", "Schedule_Id");
            AddForeignKey("dbo.ClassroomScheduleItems", "ClassroomSchedule_Id", "dbo.ClassroomSchedules", "Id");
            AddForeignKey("dbo.SemesterCourses", "Schedule_Id", "dbo.ClassroomSchedules", "Id");
            DropColumn("dbo.ClassroomSchedules", "ScheduleItem_Id");
            DropColumn("dbo.ClassroomSchedules", "SemesterCourse_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClassroomSchedules", "SemesterCourse_Id", c => c.Guid());
            AddColumn("dbo.ClassroomSchedules", "ScheduleItem_Id", c => c.Guid());
            DropForeignKey("dbo.SemesterCourses", "Schedule_Id", "dbo.ClassroomSchedules");
            DropForeignKey("dbo.ClassroomScheduleItems", "ClassroomSchedule_Id", "dbo.ClassroomSchedules");
            DropIndex("dbo.SemesterCourses", new[] { "Schedule_Id" });
            DropIndex("dbo.ClassroomScheduleItems", new[] { "ClassroomSchedule_Id" });
            DropColumn("dbo.SemesterCourses", "Schedule_Id");
            DropColumn("dbo.ClassroomScheduleItems", "ClassroomSchedule_Id");
            CreateIndex("dbo.ClassroomSchedules", "SemesterCourse_Id");
            CreateIndex("dbo.ClassroomSchedules", "ScheduleItem_Id");
            AddForeignKey("dbo.ClassroomSchedules", "SemesterCourse_Id", "dbo.SemesterCourses", "Id");
            AddForeignKey("dbo.ClassroomSchedules", "ScheduleItem_Id", "dbo.ClassroomScheduleItems", "Id");
        }
    }
}
