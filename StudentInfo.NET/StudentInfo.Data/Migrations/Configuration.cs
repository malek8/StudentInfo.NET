namespace StudentInfo.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StudentInfo.Dto;
    using StudentInfo.Enums;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentInfo.Data.StudentInfoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StudentInfo.Data.StudentInfoContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            CreateUserRoles(context);
            CreateUserAccounts(context);
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
              },
              new ApplicationUser
              {
                  Email = "mr.garrison@southpark.com",
                  UserName = "mr.garrison@southpark.com",
                  FirstName = "Mr.Garrison",
                  LastName = "ABC",
                  PasswordHash = hashedPassword,
                  SecurityStamp = Guid.NewGuid().ToString(),
                  EmailConfirmed = true
              },
              new ApplicationUser
              {
                  Email = "donald.duck@disney.com",
                  UserName = "donald.duck@disney.com",
                  FirstName = "Donald",
                  LastName = "Duck",
                  PasswordHash = hashedPassword,
                  SecurityStamp = Guid.NewGuid().ToString(),
                  EmailConfirmed = true
              }
            );
        }
    }
}
