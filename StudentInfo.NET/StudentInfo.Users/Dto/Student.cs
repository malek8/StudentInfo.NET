using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Users.Dto
{
  public class Student
  {
    public Guid Id { get; set; }
    public Guid ApplicationUserId { get; set; }
    public virtual IEnumerable<StudentCourse> StudentCourses { get; set; }
  }
}
