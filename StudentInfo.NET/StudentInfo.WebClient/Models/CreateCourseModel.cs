using StudentInfo.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient.Models
{
    public class CreateCourseModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Credits")]
        public int NumberOfCredits { get; set; }

        public ProgramLevel Level { get; set; }

        [Display(Name = "Department")]
        public Guid DepartmentId { get; set; }
    }
}