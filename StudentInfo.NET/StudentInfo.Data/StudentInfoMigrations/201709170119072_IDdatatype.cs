namespace StudentInfo.Data.StudentInfoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDdatatype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            DropPrimaryKey("dbo.Departments");
            DropPrimaryKey("dbo.Faculties");
            AlterColumn("dbo.Departments", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Departments", "Faculty_Id", c => c.Guid());
            AlterColumn("dbo.Faculties", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Departments", "Id");
            AddPrimaryKey("dbo.Faculties", "Id");
            CreateIndex("dbo.Departments", "Faculty_Id");
            AddForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            DropPrimaryKey("dbo.Faculties");
            DropPrimaryKey("dbo.Departments");
            AlterColumn("dbo.Faculties", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Departments", "Faculty_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Departments", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Faculties", "Id");
            AddPrimaryKey("dbo.Departments", "Id");
            CreateIndex("dbo.Departments", "Faculty_Id");
            AddForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties", "Id");
        }
    }
}
