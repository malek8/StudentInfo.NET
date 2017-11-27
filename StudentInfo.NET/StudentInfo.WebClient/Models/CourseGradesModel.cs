using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Users.Dto;

namespace StudentInfo.WebClient.Models
{
  public class CourseGradesModel
  {
    public SemesterCourse SemesterCourse { get; set; }

    public IEnumerable<StudentCourse> Students { get; set; }
  }
}