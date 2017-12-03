using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

        public Guid CreateStudent(string userId, Guid programId)
        {
            var applicationUserId = Guid.Parse(userId);

            if (!StudentExists(applicationUserId))
            {
                var program = _db.Programs.FirstOrDefault(x => x.Id == programId);

                if (program != null)
                {
                    var externalStudentId = GenerateStudentId();
                    var student = new Student
                    {
                        ApplicationUserId = applicationUserId,
                        Balance = 0,
                        Program = program,
                        ExternalStudentId = externalStudentId
                    };

                    _db.Students.Add(student);

                    try
                    {
                        _db.SaveChanges();

                        return student.Id;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return Guid.Empty;
        }

        private Student FindStudent(Guid userId)
        {
            var student = _db.Students.FirstOrDefault(x => x.ApplicationUserId == userId);

            return student;
        }

        private bool StudentExists(Guid userId)
        {
            return _db.Students.Any(x => x.ApplicationUserId == userId);
        }

        private long GenerateStudentId()
        {
            var lastStudent = _db.Students.OrderByDescending(x => x.ExternalStudentId).FirstOrDefault();
            if (lastStudent != null)
            {
                return lastStudent.ExternalStudentId + 5;
            }
            return 234789;
        }
    }
}
