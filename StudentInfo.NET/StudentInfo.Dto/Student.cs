﻿using StudentInfo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Dto
{
    public class Student
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public long ExternalStudentId { get; set; }
        public decimal Balance { get; set; }
        public virtual Term StartTerm { get; set; }
        public virtual int StartYear { get; set; }
        public virtual Program Program { get; set; }
        public virtual IList<StudentCourse> StudentCourses { get; set; }
        public virtual IList<Payment> Payments { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
