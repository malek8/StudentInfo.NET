using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Users.Dto
{
    public class Teacher : BaseClass
    {
        public Guid ApplicationUserId { get; set; }
        public virtual IEnumerable<TeacherCourse> TeacherCourses { get; set; }
    }
}
