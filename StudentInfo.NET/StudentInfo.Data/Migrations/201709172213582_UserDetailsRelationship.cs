namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetailsRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserDetails", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserDetails", "ApplicationUser_Id");
            AddForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserDetails", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserDetails", "ApplicationUser_Id");
        }
    }
}
