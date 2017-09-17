namespace StudentInfo.Data.StudentInfoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facultyDepartmentNavigation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            AlterColumn("dbo.Departments", "Faculty_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Departments", "Faculty_Id");
            AddForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            AlterColumn("dbo.Departments", "Faculty_Id", c => c.Guid());
            CreateIndex("dbo.Departments", "Faculty_Id");
            AddForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties", "Id");
        }
    }
}
