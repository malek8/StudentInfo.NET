namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payments1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Term = c.Int(nullable: false),
                        Student_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.PaymentItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Payment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.PaymentItems", "Payment_Id", "dbo.Payments");
            DropIndex("dbo.PaymentItems", new[] { "Payment_Id" });
            DropIndex("dbo.Payments", new[] { "Student_Id" });
            DropTable("dbo.PaymentItems");
            DropTable("dbo.Payments");
        }
    }
}
