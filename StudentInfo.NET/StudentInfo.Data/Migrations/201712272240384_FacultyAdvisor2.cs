namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacultyAdvisor2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacultyAdvisors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Faculty_Id = c.Guid(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Faculty_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacultyAdvisors", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FacultyAdvisors", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.FacultyAdvisors", new[] { "User_Id" });
            DropIndex("dbo.FacultyAdvisors", new[] { "Faculty_Id" });
            DropTable("dbo.FacultyAdvisors");
        }
    }
}
