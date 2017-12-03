namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Program_Id", c => c.Guid());
            CreateIndex("dbo.Students", "Program_Id");
            AddForeignKey("dbo.Students", "Program_Id", "dbo.Programs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Program_Id", "dbo.Programs");
            DropIndex("dbo.Students", new[] { "Program_Id" });
            DropColumn("dbo.Students", "Program_Id");
        }
    }
}
