using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.Models
{
    public class CourseEnrollModel
    {
        public Guid CourseId { get; set; }
        public Term Term { get; set; }
        public DateTime CourseDate { get; set; }
    }
}