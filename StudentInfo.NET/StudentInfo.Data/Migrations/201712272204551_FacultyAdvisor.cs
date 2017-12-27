namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacultyAdvisor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacultyAdvisor",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Faculty_Id = c.Guid(),
                    ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacultyAdvisor", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.FacultyAdvisor", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropTable("dbo.FacultyAdvisor");
        }
    }
}
