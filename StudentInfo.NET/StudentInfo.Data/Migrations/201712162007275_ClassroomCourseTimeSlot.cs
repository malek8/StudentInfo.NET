namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassroomCourseTimeSlot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassroomCourses", "TimeSlotFrom", c => c.DateTime(nullable: false));
            AddColumn("dbo.ClassroomCourses", "TimeSlotTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassroomCourses", "TimeSlotTo");
            DropColumn("dbo.ClassroomCourses", "TimeSlotFrom");
        }
    }
}
