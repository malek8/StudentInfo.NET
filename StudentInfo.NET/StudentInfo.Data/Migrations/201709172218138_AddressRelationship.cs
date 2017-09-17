namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressRelationship : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.UserDetails", "HomeAddress_Id", c => c.Guid());
            AddColumn("dbo.UserDetails", "MailAddress_Id", c => c.Guid());
            CreateIndex("dbo.UserDetails", "HomeAddress_Id");
            CreateIndex("dbo.UserDetails", "MailAddress_Id");
            AddForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses");
            DropIndex("dbo.UserDetails", new[] { "MailAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "HomeAddress_Id" });
            DropColumn("dbo.UserDetails", "MailAddress_Id");
            DropColumn("dbo.UserDetails", "HomeAddress_Id");
            DropTable("dbo.Addresses");
        }
    }
}
