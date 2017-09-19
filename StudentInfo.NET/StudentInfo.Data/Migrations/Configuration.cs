namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using StudentInfo.Users.Dto;
    using StudentInfo.Faculties;
    using StudentInfo.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentInfoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentInfoContext context)
        {
            CreateUserRoles(context);
            CreateUserAccounts(context);
            CreateFaculties(context);
        }

        private void CreateUserRoles(StudentInfoContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = SystemRoles.Administrator },
                new IdentityRole { Name = SystemRoles.Student },
                new IdentityRole { Name = SystemRoles.Instructor },
                new IdentityRole { Name = SystemRoles.FacultyMember },
                new IdentityRole { Name = SystemRoles.Advisor });
        }

        private void CreateUserAccounts(StudentInfoContext context)
        {
            var hashedPassword = new PasswordHasher().HashPassword("Playit@Playit2");

            context.Users.AddOrUpdate(
              p => p.Email,
              new ApplicationUser
              {
                  Email = "malek.atwiz@hotmail.com",
                  UserName = "malek.atwiz@hotmail.com",
                  FirstName = "Malek",
                  LastName = "A",
                  PasswordHash = hashedPassword,
                  SecurityStamp = Guid.NewGuid().ToString(),
                  EmailConfirmed = true
              },
              new ApplicationUser
              {
                  Email = "bruce.wayne@batman.com",
                  UserName = "bruce.wayne@batman.com",
                  FirstName = "Bruce",
                  LastName = "Wayne",
                  PasswordHash = hashedPassword,
                  SecurityStamp = Guid.NewGuid().ToString(),
                  EmailConfirmed = true
              }
            );
        }

        private void CreateFaculties(StudentInfoContext context)
        {
            context.Faculties.AddOrUpdate(f =>
            f.Name,
            new Faculty
            {
                Id = Guid.NewGuid(),
                Name = "Faculty of Arts and Science"
            },
            new Faculty
            {
                Id = Guid.NewGuid(),
                Name = "Faculty of Engineering and Computer Science",
                Departments = new List<Department>
                {
                    new Department{Id = Guid.NewGuid(), Name = "Building, Civil and Environmental Engineering" },
                    new Department{Id = Guid.NewGuid(), Name = "Electrical and Computer Engineering",
                    Programs = new List<Program>
                    {
                        new Program{Id = Guid.NewGuid(), Name = "Computer Engineering (BEng)", Level = Enums.ProgramLevel.Undergraduate, DegreeType = Enums.DegreeType.Major, StudyType = Enums.ProgramStudyType.CourseBased },
                        new Program{Id = Guid.NewGuid(), Name = "Electrical Engineering (BEng)", Level = Enums.ProgramLevel.Undergraduate, DegreeType = Enums.DegreeType.Major, StudyType = Enums.ProgramStudyType.CourseBased },
                        new Program{Id = Guid.NewGuid(), Name = "Electrical & Computer Engineering (MASc)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.ThesisBased },
                        new Program{Id = Guid.NewGuid(), Name = "Electrical & Computer Engineering (MEng)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.CourseBased },
                        new Program{Id = Guid.NewGuid(), Name = "Electrical & Computer Engineering (PhD)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Doctorate, StudyType = Enums.ProgramStudyType.ThesisBased }
                    } },
                    new Department{Id = Guid.NewGuid(), Name = "Mechanical, Industrial and Aerospace Engineering" },
                    new Department{Id = Guid.NewGuid(), Name = "Centre for Engineering in Society" },
                    new Department{Id = Guid.NewGuid(), Name = "Computer Science & Software Engineering",
                    Programs = new List<Program>
                    {
                        new Program{Id = Guid.NewGuid(), Name = "Master in Computer Science (MCompSc)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.ThesisBased },
                        new Program{Id = Guid.NewGuid(), Name = "Master in Applied Science, Software Engineering (MASc)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.ThesisBased },
                        new Program{Id = Guid.NewGuid(), Name = "Master of Engineering, Software Engineering (MEng)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.CourseBased },
                        new Program{Id = Guid.NewGuid(), Name = "Master in Applied Computer Science (MApCompSc)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.Master, StudyType = Enums.ProgramStudyType.CourseBased },
                        new Program{Id = Guid.NewGuid(), Name = "Graduate diploma in Computer Science (GrDip)", Level = Enums.ProgramLevel.Graduate, DegreeType = Enums.DegreeType.GraduateDiploma, StudyType = Enums.ProgramStudyType.CourseBased }
                    } },
                    new Department{Id = Guid.NewGuid(), Name = "Concordia Institute for Information Systems Engineering (CIISE)" },
                    new Department{Id = Guid.NewGuid(), Name = "Concordia Institute for Aerospace Design and Innovation" }
                }
            },
            new Faculty
            {
                Id = Guid.NewGuid(),
                Name = "Faculty of Fine Arts"
            },
            new Faculty
            {
                Id = Guid.NewGuid(),
                Name = "John Molson School of Business",
                Departments = new List<Faculties.Department>
                {
                    new Department{Id = Guid.NewGuid(), Name = "Accountancy" },
                    new Department{Id = Guid.NewGuid(), Name = "Finance" },
                    new Department{Id = Guid.NewGuid(), Name = "Management" },
                    new Department{Id = Guid.NewGuid(), Name = "Marketing" },
                    new Department{Id = Guid.NewGuid(), Name = "Supply Chain & Business Technology Management" }
                }
            });
        }
    }
}
