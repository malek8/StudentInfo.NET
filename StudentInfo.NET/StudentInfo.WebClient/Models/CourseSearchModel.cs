using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudentInfo.Dto;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.Models
{
    public class CourseSearchModel
    {
        public string Keyword { get; set; }

        public string Code { get; set; }

        [Display(Name = "Department")]
        public Guid? DepartmentId { get; set; }

        [Display(Name = "Faculty")]
        public Guid? FacultyId { get; set; }

        public Guid? StudentId { get; set; }

        [Display(Name = "Term")]
        public Term? Semester { get; set; }

        public PagedList.IPagedList<SemesterCourse> Results { get; set; }

        public PagedList.IPagedList<Course> CoursesResults { get; set; }

        public PagedList.IPagedList<StudentCourse> StudentCourses { get; set; }
    }
}