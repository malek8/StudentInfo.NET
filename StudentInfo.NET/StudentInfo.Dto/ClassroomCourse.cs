using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class ClassroomCourse
    {
        public Guid Id { get; set; }
        public DateTime TimeSlotFrom { get; set; }
        public DateTime TimeSlotTo { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual IList<SemesterCourse> SemesterCourses { get; set; }
    }
}
