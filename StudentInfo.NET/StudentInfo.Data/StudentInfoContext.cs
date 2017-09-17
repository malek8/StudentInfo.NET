using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Faculties;
using StudentInfo.Users.Dto;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StudentInfo.Data
{
    public class StudentInfoContext : IdentityDbContext
    {
        public StudentInfoContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<ApplicationUser>().HasKey(x => x.Id);
            //modelBuilder.Entity<ApplicationUser>().Ignore(x => x.Logins);
            //modelBuilder.Entity<ApplicationUser>().Ignore(x => x.Roles);
            //modelBuilder.Entity<ApplicationUser>().Ignore(x => x.Claims);

            //modelBuilder.Entity<Department>().HasRequired(x => x.Faculty);
            //modelBuilder.Entity<Program>().HasRequired(x => x.Department);

            //modelBuilder.Entity<UserDetails>().HasRequired(x => x.ApplicationUser);
            //modelBuilder.Entity<User>().HasOptional(x => x.HomeAddress);
            //modelBuilder.Entity<User>().HasOptional(x => x.MailAddress);
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<UserDetails> UserDetails { get; set; }
    }
}
