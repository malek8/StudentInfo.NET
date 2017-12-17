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

        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public Term Term { get; set; }

        [Display(Name = "Classroom")]
        public Guid ClassroomCourseId { get; set; }

        [Display(Name = "Instructor")]
        public Guid InstructorId { get; set; }

        public bool ClassIsAvailable { get; set; }
        public Guid SelectedClassId { get; set; }
    }
}