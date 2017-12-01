namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.StudentCourses", "Paid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentCourses", "Paid", c => c.Boolean(nullable: false));
            DropColumn("dbo.Students", "Balance");
        }
    }
}
