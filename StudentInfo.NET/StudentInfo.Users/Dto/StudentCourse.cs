using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;

namespace StudentInfo.Users.Dto
{
    public class StudentCourse
    {
        public Guid Id { get; set; }
        public Course Course { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime CourseDate { get; set; }
        public CourseRegistrationState CourseState { get; set; }
        public Term Term { get; set; }
    }
}
