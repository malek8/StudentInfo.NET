using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.Models
{
    public class EditCourseModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Credits")]
        public int NumberOfCredits { get; set; }

        [Display(Name = "Level")]
        public ProgramLevel Level { get; set; }
    }
}