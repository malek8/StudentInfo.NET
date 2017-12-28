namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Students", "User_Id");
            AddForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "User_Id" });
            DropColumn("dbo.Students", "User_Id");
        }
    }
}
