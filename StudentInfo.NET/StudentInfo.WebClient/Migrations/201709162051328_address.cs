namespace StudentInfo.WebClient.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAddress_AddressLine1", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAddress_AddressLine2", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAddress_City", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAddress_Province", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeAddress_Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "MailAddress_AddressLine1", c => c.String());
            AddColumn("dbo.AspNetUsers", "MailAddress_AddressLine2", c => c.String());
            AddColumn("dbo.AspNetUsers", "MailAddress_City", c => c.String());
            AddColumn("dbo.AspNetUsers", "MailAddress_Province", c => c.String());
            AddColumn("dbo.AspNetUsers", "MailAddress_Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MailAddress_Country");
            DropColumn("dbo.AspNetUsers", "MailAddress_Province");
            DropColumn("dbo.AspNetUsers", "MailAddress_City");
            DropColumn("dbo.AspNetUsers", "MailAddress_AddressLine2");
            DropColumn("dbo.AspNetUsers", "MailAddress_AddressLine1");
            DropColumn("dbo.AspNetUsers", "HomeAddress_Country");
            DropColumn("dbo.AspNetUsers", "HomeAddress_Province");
            DropColumn("dbo.AspNetUsers", "HomeAddress_City");
            DropColumn("dbo.AspNetUsers", "HomeAddress_AddressLine2");
            DropColumn("dbo.AspNetUsers", "HomeAddress_AddressLine1");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.AspNetUsers", "FirstName");
        }
    }
}
