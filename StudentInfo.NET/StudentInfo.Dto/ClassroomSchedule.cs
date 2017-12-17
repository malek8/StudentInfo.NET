using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class ClassroomSchedule
    {
        public Guid Id { get; set; }
        public virtual Classroom Classroom { get; set; }
        public virtual IList<ClassroomScheduleItem> ScheduleItems { get; set; }
    }
}
