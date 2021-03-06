﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;

namespace StudentInfo.Dto
{
    public class TeacherCourse : UserCourseBase
    {
        public Guid TeacherId { get; set; }
        public CourseState State { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
