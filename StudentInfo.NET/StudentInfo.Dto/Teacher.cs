using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInfo.Dto
{
  public class Teacher : BaseClass
  {
    public Guid ApplicationUserId { get; set; }
    public virtual IEnumerable<TeacherCourse> TeacherCourses { get; set; }

    [NotMapped]
    public ApplicationUser User { get; set; }
  }
}