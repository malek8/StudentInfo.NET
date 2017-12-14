namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: true, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Classrooms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.String(),
                        Campus = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Code = c.String(nullable: false, maxLength: 12),
                        Description = c.String(maxLength: 500),
                        NumberOfCredits = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Faculty_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id)
                .Index(t => t.Faculty_Id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 125),
                        Level = c.Int(nullable: false),
                        DegreeType = c.Int(nullable: false),
                        StudyType = c.Int(nullable: false),
                        TermCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
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
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.Guid(nullable: false),
                        ExternalStudentId = c.Long(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Program_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Programs", t => t.Program_Id)
                .Index(t => t.Program_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SemesterCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseDate = c.DateTime(nullable: false),
                        Term = c.Int(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Classroom_Id = c.Guid(),
                        Course_Id = c.Guid(),
                        Teacher_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classrooms", t => t.Classroom_Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.Classroom_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Grade = c.String(),
                        CourseState = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        SemesterCourse_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SemesterCourses", t => t.SemesterCourse_Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.SemesterCourse_Id);
            
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeacherId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                        SemesterCourse_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SemesterCourses", t => t.SemesterCourse_Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId)
                .Index(t => t.SemesterCourse_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PaymentMethod = c.String(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CardNumber = c.String(),
                        Payment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.Payment_Id)
                .Index(t => t.Payment_Id);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WorkPhone = c.String(maxLength: 25),
                        HomePhone = c.String(maxLength: 25),
                        CellPhone = c.String(maxLength: 25),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        HomeAddress_Id = c.Guid(),
                        MailAddress_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Addresses", t => t.HomeAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.MailAddress_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.HomeAddress_Id)
                .Index(t => t.MailAddress_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserDetails", "MailAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "HomeAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Transactions", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.TeacherCourses", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.TeacherCourses", "SemesterCourse_Id", "dbo.SemesterCourses");
            DropForeignKey("dbo.StudentCourses", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentCourses", "SemesterCourse_Id", "dbo.SemesterCourses");
            DropForeignKey("dbo.SemesterCourses", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.SemesterCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.SemesterCourses", "Classroom_Id", "dbo.Classrooms");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Payments", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Program_Id", "dbo.Programs");
            DropForeignKey("dbo.PaymentItems", "Payment_Id", "dbo.Payments");
            DropForeignKey("dbo.Programs", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Departments", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.Courses", "Department_Id", "dbo.Departments");
            DropIndex("dbo.UserDetails", new[] { "MailAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "HomeAddress_Id" });
            DropIndex("dbo.UserDetails", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Transactions", new[] { "Payment_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "SemesterCourse_Id" });
            DropIndex("dbo.TeacherCourses", new[] { "TeacherId" });
            DropIndex("dbo.StudentCourses", new[] { "SemesterCourse_Id" });
            DropIndex("dbo.StudentCourses", new[] { "StudentId" });
            DropIndex("dbo.SemesterCourses", new[] { "Teacher_Id" });
            DropIndex("dbo.SemesterCourses", new[] { "Course_Id" });
            DropIndex("dbo.SemesterCourses", new[] { "Classroom_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Students", new[] { "Program_Id" });
            DropIndex("dbo.PaymentItems", new[] { "Payment_Id" });
            DropIndex("dbo.Payments", new[] { "Student_Id" });
            DropIndex("dbo.Programs", new[] { "Department_Id" });
            DropIndex("dbo.Departments", new[] { "Faculty_Id" });
            DropIndex("dbo.Courses", new[] { "Department_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Transactions");
            DropTable("dbo.TeacherCourses");
            DropTable("dbo.StudentCourses");
            DropTable("dbo.Teachers");
            DropTable("dbo.SemesterCourses");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.PaymentItems");
            DropTable("dbo.Payments");
            DropTable("dbo.Programs");
            DropTable("dbo.Faculties");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.Classrooms");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Addresses");
        }
    }
}
