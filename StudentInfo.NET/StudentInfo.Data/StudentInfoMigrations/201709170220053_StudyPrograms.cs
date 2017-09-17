namespace StudentInfo.Data.StudentInfoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudyPrograms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 125),
                        Level = c.Int(nullable: false),
                        DegreeType = c.Int(nullable: false),
                        StudyType = c.Int(nullable: false),
                        Department_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Department_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "Department_Id", "dbo.Departments");
            DropIndex("dbo.Programs", new[] { "Department_Id" });
            DropTable("dbo.Programs");
        }
    }
}
