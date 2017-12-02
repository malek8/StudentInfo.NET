using StudentInfo.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Helpers
{
    public class CourseHelper
    {
        public static Term CurrentTerm()
        {
            var currentMonth = DateTime.Now.Month;

            switch (currentMonth)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return Term.Winter;
                case 5:
                case 6:
                    return Term.Summer1;
                case 7:
                case 8:
                    return Term.Summer2;
                case 9:
                case 10:
                case 11:
                case 12:
                    return Term.Fall;
                default:
                    return Term.Fall;
            }
        }
    }
}
