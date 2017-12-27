using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Dto;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.Models
{
    public class StudentRecordModel
    {
        public Student Student { get; set; }
        public ApplicationUser User { get; set; }
        public decimal CummulativeGPA { get; set; }
        public int TotalEarnedCredits { get; set; }
        public IList<StudentCourse> StudentCourses { get; set; }
        public IDictionary<int, IEnumerable<IGrouping<Term, StudentCourse>>> GroupedStudentCourses { get; set; }
    }
}