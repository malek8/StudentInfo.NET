using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;

namespace StudentInfo.Users.Dto
{
    public class StudentCourse : UserCourseBase
    {
        public Guid StudentId { get; set; }
        public string Grade { get; set; }
        public CourseRegistrationState CourseState { get; set; }
    }
}
