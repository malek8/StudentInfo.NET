using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;

namespace StudentInfo.Dto
{
    public class SemesterCourse
    {
        public Guid Id { get; set; }
        public DateTime CourseDate { get; set; }
        public Term Term { get; set; }
        public bool Open { get; set; }
        public virtual Course Course { get; set; }
        public virtual ClassroomCourse ClassroomCourse { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
