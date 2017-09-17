using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;
using StudentInfo.Faculties;

namespace StudentInfo.Users.Dto
{
    public class UserStudent : UserDetails
    {
        //public StudentPayRate Rate { get; set; }

        public Program Program { get; set; }

        public StudentRecord Record { get; set; }
    }

    public class StudentRecord
    {

    }
}
