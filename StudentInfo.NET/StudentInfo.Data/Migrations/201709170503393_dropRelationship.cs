namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserDetails", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserDetails", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDetails", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserDetails", "ApplicationUser_Id");
            AddForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
