﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Enums
{
    public enum StudentPayRate
    {
        International = 1,
        Canadian = 2,
        Quebecois = 3,
    }

    public enum ProgramLevel
    {
        Undergraduate = 1,
        Graduate = 2,
    }

    public enum DegreeType
    {
        [Display(Name = "Major")]
        Major = 1,

        [Display(Name = "Master")]
        Master = 2,

        [Display(Name = "Doctorate")]
        Doctorate = 3,

        [Display(Name = "Graduate Diploma")]
        GraduateDiploma = 4,
    }

    public enum ProgramStudyType
    {
        CourseBased = 1,
        ThesisBased = 2,
    }

    public enum Term
    {
        Fall = 1,
        Winter = 2,
        Summer1 = 3,
        Summer2 = 4
    }

    public enum CourseRegistrationState
    {
        Added = 1,
        Enrolled = 2,
        Waiting = 3,
        Dropped = 4,
        Pass = 5,
        Fail = 6,
        Discontinue = 7
    }

    public enum CourseState
    {
        Open = 1,
        Closed = 2,
        Completed = 3,
    }
}
