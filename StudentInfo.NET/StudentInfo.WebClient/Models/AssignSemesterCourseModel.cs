using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Dto;
using StudentInfo.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentInfo.WebClient.Models
{
    public class AssignSemesterCourseModel
    {
        public Course Course { get; set; }
        public IList<SemesterCourse> SemesterCourse { get; set; }

        [Display(Name = "Date")]
        public DateTime CourseDate { get; set; }
        public Term Term { get; set; }

        [Display(Name = "Classroom")]
        public Guid ClassroomId { get; set; }

        [Display(Name = "Cost")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
    }
}