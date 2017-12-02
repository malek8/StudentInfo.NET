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

        public Student FindByUserId(string userId)
        {
            var studentId = Guid.Parse(userId);
            return FindStudent(studentId);
        }

        public Student FindByUserId(Guid userId)
        {
            return FindStudent(userId);
        }

        public List<StudentCourse> GetAllCourses(Student student)
        {
            var courses = new List<StudentCourse>();

            if (student != null)
            {
                var studentCourses = _db.StudentCourses.Where(x => x.StudentId == student.Id);
                if (studentCourses != null)
                {
                    courses.AddRange(studentCourses);
                }
            }

            return courses;
        }

        public List<StudentCourse> GetCurrentCourses(Student student)
        {
            var courses = new List<StudentCourse>();

            var allCourses = GetAllCourses(student);
            courses.AddRange(allCourses.Where(x => x.SemesterCourse.Term == CourseHelper.CurrentTerm()));

            return courses;
        }

        public List<StudentCourse> GetCurrentStudentCourses(string userId)
        {
            var student = FindByUserId(userId);
            return GetCurrentCourses(student);
        }

        private Student FindStudent(Guid userId)
        {
            var student = _db.Students.FirstOrDefault(x => x.ApplicationUserId == userId);

            return student;
        }
    }
}
