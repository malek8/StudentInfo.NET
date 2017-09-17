namespace StudentInfo.Data.StudentInfoMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Faculty_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id)
                .Index(t => t.Faculty_Id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
        }
    }
}
