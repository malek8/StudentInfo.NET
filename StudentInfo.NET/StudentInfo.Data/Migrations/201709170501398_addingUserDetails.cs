namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        CellPhone = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        HomeAddress_Id = c.Guid(),
                        MailAddress_Id = c.Guid(),
                        Program_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Addresses", t => t.HomeAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.MailAddress_Id)
                .ForeignKey("dbo.Programs", t => t.Program_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.HomeAddress_Id)
                .Index(t => t.MailAddress_Id)
                .Index(t => t.Program_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(maxLength: 50),
                        Province = c.String(maxLength: 30),
                        PostalCode = c.String(maxLength: 6),
                        Country = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserDetails", new[] { "Program_Id" });
            DropIndex("dbo.UserDetails", new[] { "MailAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "HomeAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Addresses");
            DropTable("dbo.UserDetails");
        }
    }
}
