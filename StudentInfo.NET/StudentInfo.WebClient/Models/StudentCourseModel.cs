using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Users.Dto;

namespace StudentInfo.WebClient.Models
{
  public class StudentCourseModel
  {
    public string StudentName { get; set; }
    public long StudentExternalId { get; set; }
    public string CourseCode { get; set; }
    public string CourseTitle { get; set; }
    public string Grade { get; set; }
  }
}