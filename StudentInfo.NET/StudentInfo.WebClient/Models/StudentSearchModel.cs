﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudentInfo.Dto;

namespace StudentInfo.WebClient.Models
{
    public class StudentSearchModel
    {
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }

        [Display(Name = "Department")]
        public Guid? DepartmentId { get; set; }

        [Display(Name = "Student Name")]
        public string Name { get; set; }

        public PagedList.IPagedList<Student> Result { get; set; }
    }
}