namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClassroomSchedules", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClassroomSchedules", "Title");
        }
    }
}
