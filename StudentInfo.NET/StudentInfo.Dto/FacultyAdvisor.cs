using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class FacultyAdvisor
    {
        public Guid Id { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
