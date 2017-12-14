using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public long ExternalStudentId { get; set; }
        public decimal Balance { get; set; }
        public virtual Program Program { get; set; }
        public virtual IEnumerable<StudentCourse> StudentCourses { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
