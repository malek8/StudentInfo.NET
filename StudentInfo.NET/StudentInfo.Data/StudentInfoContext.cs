using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Faculties;

namespace StudentInfo.Data
{
    public class StudentInfoContext : DbContext
    {
        public StudentInfoContext()
            : base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Program>().HasRequired(x => x.Department);
            modelBuilder.Entity<Department>().HasRequired(x => x.Faculty);
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Program> Programs { get; set; }
    }
}
