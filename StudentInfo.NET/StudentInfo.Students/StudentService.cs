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
using StudentInfo.Enums;

namespace StudentInfo.Students
{
    public class StudentService
    {
        private StudentInfoContext _db;
        private CourseService _courseService;
        private StudentPaymentService _studentPaymentService;

        public StudentService()
        {
            _db = new StudentInfoContext();
            _courseService = new CourseService();
            _studentPaymentService = new StudentPaymentService();
        }

        public Student FindByUserId(string userId)
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
            if (student != null)
            {
                return _db.StudentCourses.Where(x => x.StudentId == student.Id).ToList();
            }
            return null;
        }

        public Guid CreateStudent(string userId, Guid programId, Term startTerm, int startYear)
        {
            if (!StudentExists(userId) && startYear >= DateTime.Now.Year)
            {
                var program = _db.Programs.FirstOrDefault(x => x.Id == programId);
                var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userId);

                if (program != null && user != null)
                {
                    var externalStudentId = GenerateStudentId();
                    var student = new Student
                    {
                        Id = Guid.NewGuid(),
                        User = user,
                        Balance = 0,
                        StartTerm = startTerm,
                        StartYear = startYear,
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

        public bool Enroll(Guid studentId, Guid courseSemesterId, out string message)
        {
            message = string.Empty;
            var student = _db.Students.Include(x => x.Program.Department).FirstOrDefault(x => x.Id == studentId);
            var courseSemester = _db.SemesterCourses.Find(courseSemesterId);
            if (student != null && courseSemester != null && courseSemester.Open)
            {
                if (!EnrolledInCourse(student, courseSemester))
                {
                    if (IsCourseAvailable(courseSemester))
                    {
                        if (CanAddCourses(student, courseSemester))
                        {
                            if (HasSameStudyLevel(student, courseSemester))
                            {
                                if (!IsConflict(courseSemester, student))
                                {
                                    if (!IsClassFull(courseSemester))
                                    {
                                        var studentCourse = new StudentCourse
                                        {
                                            Student = student,
                                            SemesterCourse = courseSemester,
                                            CreateDate = DateTime.Now,
                                            LastUpdate = DateTime.Now,
                                            CourseState = CourseRegistrationState.Enrolled,
                                            Id = Guid.NewGuid()
                                        };

                                        _db.StudentCourses.Add(studentCourse);

                                        try
                                        {
                                            _db.SaveChanges();

                                            var year = DateTime.Now.Year;
                                            var scheduleItem = courseSemester.Schedule.ScheduleItems.FirstOrDefault();
                                            if (scheduleItem != null)
                                            {
                                                year = scheduleItem.Date.Year;
                                            }

                                            if (!HasTermPayment(student, year, courseSemester.Term))
                                            {
                                                _studentPaymentService.InitTermPayment(student.Id, courseSemester.Term, year);
                                            }

                                            message = $"{courseSemester.Course.Name} was added successfully";
                                            return true;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        message = "Sorry, the class is full.";
                                    }
                                }
                                else
                                {
                                    message = "This course conflicts with an existing course";
                                }
                            }
                            else
                            {
                                message = "Sorry, you cannot add this course";
                            }
                        }
                        else
                        {
                            message = "Sorry, you cannot take this course, contact program advisor";
                        }
                    }
                    else
                    {
                        message = "Sorry, course is not available for registration now";
                    }
                }
                else
                {
                    message = "You were already enrolled in this course";
                }
            }
            else
            {
                message = "Sorry, something went worng or course is not available";
            }
            return false;
        }

        public bool Drop(Guid studentId, Guid studentCourseId, out string message)
        {
            message = string.Empty;
            var student = _db.Students.Find(studentId);
            var studentCourse = _db.StudentCourses.Find(studentCourseId);

            if (studentCourse != null && student != null)
            {
                var thirdClass = studentCourse.SemesterCourse.Schedule.ScheduleItems.
                    OrderBy(x => x.Date).Skip(2).FirstOrDefault();

                if (thirdClass != null)
                {
                    if (DateTime.Now >= thirdClass.Date)
                    {
                        message = "Sorry, you cannot drop this course";
                        return false;
                    }
                }

                if (!string.IsNullOrEmpty(studentCourse.Grade) || 
                    studentCourse.CourseState == CourseRegistrationState.Pass)
                {
                    return false;
                }

                studentCourse.CourseState = Enums.CourseRegistrationState.Discontinue;

                try
                {
                    _db.SaveChanges();

                    message = $"{studentCourse.SemesterCourse.Course.Name} was dropped successfully";
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                message = "Sorry, something went wrong";
            }

            return false;
        }

        private Student FindStudent(string userId)
        {
            var student = _db.Students.Include(x => x.StudentCourses)
                .FirstOrDefault(x => x.User.Id == userId);

            return student;
        }

        private bool StudentExists(string userId)
        {
            return _db.Students.Any(x => x.User.Id == userId);
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
            if (StudentHelper.AvailableTerms().Contains(semesterCourse.Term))
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
            x.SemesterCourse.Term == semesterCourse.Term && x.CourseState == CourseRegistrationState.Enrolled).ToList();

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

        private bool IsConflict(SemesterCourse semesterCourse, Student student)
        {
            var studentCourses = _db.StudentCourses.Where(x => x.StudentId == student.Id 
            && x.SemesterCourse.Term == semesterCourse.Term && x.CourseState == CourseRegistrationState.Enrolled).ToList();

            if (studentCourses != null)
            {
                foreach (var course in studentCourses)
                {
                    var scheduleItems = course.SemesterCourse.Schedule.ScheduleItems;

                    foreach(var item in scheduleItems)
                    {
                        var sameDay = semesterCourse.Schedule.ScheduleItems.Where(x => x.Date == item.Date);

                        if (sameDay != null)
                        {
                            foreach(var d in sameDay)
                            {
                                if (Math.Abs(d.EndTime.Subtract(item.EndTime).Hours) < 3)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private bool HasTermPayment(Student student, int year, Term term)
        {
            if (_db.Payments.Any(x => x.Student.Id == student.Id && term == term && x.DueDate.Year == year))
            {
                return true;
            }
            return false;
        }

        private bool EnrolledInCourse(Student student, SemesterCourse semesterCourse)
        {
            return _db.StudentCourses.Any(x => x.StudentId == student.Id
            && x.SemesterCourse.Course.Id == semesterCourse.Course.Id);
        }

        private bool HasSameStudyLevel(Student student, SemesterCourse semesterCourse)
        {
            return (student.Program.Level == semesterCourse.Course.Level);
        }

        private bool IsClassFull(SemesterCourse semesterCourse)
        {
            var currentCount = _db.StudentCourses.Count(x => x.SemesterCourse.Id == semesterCourse.Id);
            if (semesterCourse.Schedule.Classroom.Capacity < currentCount)
            {
                return false;
            }
            return true;
        }
    }
}
