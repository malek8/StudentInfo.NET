using StudentInfo.Enums;
using StudentInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Students
{
    public static class StudentHelper
    {
        public static bool IsPaymentDue()
        {
            var currentDate = DateTime.Now;

            switch(currentDate.Month)
            {
                case 2:
                case 3:
                case 4:
                    return true;
                case 6:
                    return true;
                case 8:
                    return true;
                case 10:
                case 11:
                case 12:
                    return true;
                default:
                    return false;
            }
        }

        public static string PaymentDueDate(Term term)
        {
            switch (term)
            {
                case Term.Fall:
                    return $"Sep 30, {DateTime.Now.Year}";
                case Term.Winter:
                    return $"Jan 31, {DateTime.Now.Year}";
                case Term.Summer1:
                    return $"May 31, {DateTime.Now.Year}";
                case Term.Summer2:
                    return $"Jul 31, {DateTime.Now.Year}";
                default:
                    return string.Empty;
            }
        }

        public static DateTime DueDate()
        {
            var currentTerm = CourseHelper.CurrentTerm();
            var currentYear = DateTime.Now.Year;
            var date = DateTime.Now;

            switch(currentTerm)
            {
                case Term.Fall:
                    date = new DateTime(currentYear, 9, 30);
                    if (DateTime.Now.Month > 9) date.AddYears(1);
                    return date;
                case Term.Winter:
                    date = new DateTime(currentYear, 1, 31);
                    if (DateTime.Now.Month > 1) date.AddYears(1);
                    return date;
                case Term.Summer1:
                    date = new DateTime(currentYear, 5, 31);
                    if (DateTime.Now.Month > 5) date.AddYears(1);
                    return date;
                case Term.Summer2:
                    date = new DateTime(currentYear, 7, 31);
                    if (DateTime.Now.Month > 7) date.AddYears(1);
                    return date;
            }
            return date;
        }
    }
}
