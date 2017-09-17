namespace StudentInfo.Data.StudentInfoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Faculties", "Name", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Faculties", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
