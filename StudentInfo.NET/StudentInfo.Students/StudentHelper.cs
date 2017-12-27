using StudentInfo.Enums;
using StudentInfo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Dto;

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

        public static DateTime DueDate(Term term, int year)
        {
            var date = DateTime.Now;

            switch(term)
            {
                case Term.Fall:
                    date = new DateTime(year, 9, 30);
                    return date;
                case Term.Winter:
                    date = new DateTime(year, 1, 31);
                    return date;
                case Term.Summer1:
                    date = new DateTime(year, 5, 31);
                    return date;
                case Term.Summer2:
                    date = new DateTime(year, 7, 31);
                    return date;
            }
            return date;
        }

        public static List<Term> AvailableTerms()
        {
            var currentMonth = DateTime.Now.Month;
            List<Term> terms = new List<Term>();

            if (currentMonth > 3 && currentMonth  <= 6)
            {
                terms.Add(Term.Summer1);
                terms.Add(Term.Summer2);
            }
            else if (currentMonth > 6)
            {
                terms.Add(Term.Fall);
                terms.Add(Term.Winter);
            }

            return terms;
        }

        public static decimal CummulativeGPA(List<StudentCourse> studentCourses)
        {
            var cummulativeGrades = 0.00m;
            var totalCredits = 0;

            foreach (var item in studentCourses.Where(x => x.Grade != null))
            {
                var grade = 0.00m;
                if (!string.IsNullOrEmpty(item.Grade))
                {
                    grade = Grade.GetAverage(item.Grade);
                }

                cummulativeGrades += item.SemesterCourse.Course.NumberOfCredits * grade;
                totalCredits += item.SemesterCourse.Course.NumberOfCredits;
            }

            if (cummulativeGrades > 0)
            {
                return cummulativeGrades / totalCredits;
            }
            return 0;
        }

        public static int TotalEarnedCredits(List<StudentCourse> studentCourses)
        {
            var totalCredits = 0;

            foreach(var item in studentCourses.Where(x => x.Grade != null))
            {
                if (!string.IsNullOrEmpty(item.Grade) && 
                    !item.Grade.Equals("F", StringComparison.InvariantCultureIgnoreCase))
                {
                    totalCredits += item.SemesterCourse.Course.NumberOfCredits;
                }
            }
            return totalCredits;
        }
    }
}
