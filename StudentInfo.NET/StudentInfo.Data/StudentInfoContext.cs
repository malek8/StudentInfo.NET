using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Dto;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StudentInfo.Data
{
    public class StudentInfoContext : IdentityDbContext
    {
        public StudentInfoContext()
            : base()
        {
            this.Configuration.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<SemesterCourse> SemesterCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TeacherCourse> TeacherCourses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ClassroomSchedule> ClassroomSchedules { get; set; }
    }
}
