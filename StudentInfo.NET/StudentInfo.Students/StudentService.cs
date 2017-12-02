using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;
using StudentInfo.Users.Dto;
using StudentInfo.Helpers;

namespace StudentInfo.Students
{
    public class StudentService
    {
        private StudentInfoContext _db;

        public StudentService()
        {
            _db = new StudentInfoContext();
        }

        public Student FindByUserId(string id)
        {
            var studentId = Guid.Parse(id);
            return FindStudent(studentId);
        }

        public Student FindByUserId(Guid id)
        {
            return FindStudent(id);
        }

        public List<StudentCourse> GetAllCourses(Student student)
        {
            var courses = new List<StudentCourse>();

            if (student != null)
            {
                if (student.StudentCourses != null)
                {
                    courses.AddRange(student.StudentCourses);
                }
            }

            return courses;
        }

        public List<StudentCourse> GetCurrentCourses(Student student)
        {
            var courses = new List<StudentCourse>();

            if (student != null)
            {
                if (student.StudentCourses != null)
                {
                    var allCourses = GetAllCourses(student);
                    courses.AddRange(allCourses.Where(x => x.SemesterCourse.Term == CourseHelper.CurrentTerm()));
                }
            }

            return courses;
        }

        public List<StudentCourse> GetCurrentStudentCourses(string id)
        {
            var student = FindByUserId(id);
            return GetCurrentCourses(student);
        }

        private Student FindStudent(Guid id)
        {
            var student = _db.Students.Find(id);

            return student;
        }
    }
}
