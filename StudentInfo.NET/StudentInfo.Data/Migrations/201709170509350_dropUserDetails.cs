namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropUserDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "Program_Id", "dbo.Programs");
            DropIndex("dbo.UserDetails", new[] { "HomeAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "MailAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "Program_Id" });
            DropTable("dbo.UserDetails");
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomePhone = c.String(),
                        WorkPhone = c.String(),
                        CellPhone = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        HomeAddress_Id = c.Guid(),
                        MailAddress_Id = c.Guid(),
                        Program_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserDetails", "Program_Id");
            CreateIndex("dbo.UserDetails", "MailAddress_Id");
            CreateIndex("dbo.UserDetails", "HomeAddress_Id");
            AddForeignKey("dbo.UserDetails", "Program_Id", "dbo.Programs", "Id");
            AddForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses", "Id");
        }
    }
}
