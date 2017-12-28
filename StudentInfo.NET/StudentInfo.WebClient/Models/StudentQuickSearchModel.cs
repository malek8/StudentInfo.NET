using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Dto;

namespace StudentInfo.WebClient.Models
{
    public class StudentQuickSearchModel
    {
        public IList<Student> Students { get; set; }
        public SemesterCourse SemesterCourse { get; set; }
    }
}