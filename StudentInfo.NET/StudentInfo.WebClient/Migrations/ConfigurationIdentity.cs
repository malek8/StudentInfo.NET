namespace StudentInfo.WebClient.Migrations
{
    using StudentInfo.WebClient.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ConfigurationIdentity : DbMigrationsConfiguration<StudentInfo.WebClient.Models.ApplicationDbContext>
    {
        public ConfigurationIdentity()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(StudentInfo.WebClient.Models.ApplicationDbContext context)
        {
            context.Users.AddOrUpdate(
              p => p.Email,
              new ApplicationUser
              {
                  Email = "malek.atwiz@hotmail.com",
                  UserName = "malek.atwiz@hotmail.com",
                  FirstName = "Malek",
                  LastName = "A",
                  PasswordHash = "AJcag5W34+9EsdoD8LVANbNFlmBRzN7UYQK/w53BShVLO1VXd+jkNbvJCye/PYUqtQ==",
                  SecurityStamp = "820248a4-8273-4cf2-9aa8-80662168c6ea",
                  EmailConfirmed = true
              }
            );
        }
    }
}
