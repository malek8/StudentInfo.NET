using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;
using StudentInfo.Dto;
using StudentInfo.Helpers;
using StudentInfo.CourseManager;

namespace StudentInfo.Students
{
    public class StudentService
    {
        private StudentInfoContext _db;
        private CourseService _courseService;

        public StudentService()
        {
            _db = new StudentInfoContext();
            _courseService = new CourseService();
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
                        Id = Guid.NewGuid(),
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

        public bool Enroll(Guid studentId, Guid courseSemesterId)
        {
            var student = _db.Students.Find(studentId);
            var courseSemester = _db.SemesterCourses.Find(courseSemesterId);
            if (student != null && courseSemester != null && courseSemester.Open)
            {
                if (IsCourseAvailable(courseSemester) && CanAddCourses(student, courseSemester))
                {
                    var studentCourse = new StudentCourse
                    {
                        Student = student,
                        SemesterCourse = courseSemester,
                        CreateDate = DateTime.Now,
                        LastUpdate = DateTime.Now,
                        CourseState = Enums.CourseRegistrationState.Enrolled,
                        Id = Guid.NewGuid()
                    };

                    _db.StudentCourses.Add(studentCourse);

                    try
                    {
                        _db.SaveChanges();

                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
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

        private bool IsCourseAvailable(SemesterCourse semesterCourse)
        {
            var currentTerm = CourseHelper.CurrentTerm();
            var nextTerm = CourseHelper.NextTerm();

            if (semesterCourse.Term == currentTerm || semesterCourse.Term == nextTerm)
            {
                var schedule = semesterCourse.Schedule;
                if (schedule != null)
                {
                    var classScheduleItem = schedule.ScheduleItems.FirstOrDefault();
                    if (classScheduleItem != null)
                    {
                        if (classScheduleItem.Date > DateTime.Now)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CanAddCourses(Student student, SemesterCourse semesterCourse, bool allowDifferentProgram = false)
        {
            var studentCourses = _db.StudentCourses.Where(x => x.StudentId == student.Id &&
            x.SemesterCourse.Term == semesterCourse.Term).ToList();

            if (studentCourses != null)
            {
                if (studentCourses.Any(x => x.SemesterCourse.Id == semesterCourse.Id) ||
                    studentCourses.Count >= 3)
                return false;
            }

            if (!allowDifferentProgram && semesterCourse.Course.Department.Id != student.Program.Department.Id)
            {
                return false;
            }

            return true;
        }
    }
}
