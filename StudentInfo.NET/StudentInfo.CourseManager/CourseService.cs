using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;
using StudentInfo.Dto;
using StudentInfo.Enums;

namespace StudentInfo.CourseManager
{
    public class CourseService
    {
        private StudentInfoContext _db;
        private ClassroomService _classroomService;

        public CourseService()
        {
            _db = new StudentInfoContext();
            _classroomService = new ClassroomService();
        }

        public Course CreateCourse(string code, string name, string desc, int credits, ProgramLevel level, Department department)
        {
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !IsCourseExists(code) && credits > 0)
            {
                var d = _db.Departments.FirstOrDefault(x => x.Id == department.Id);
                var course = new Course
                {
                    Id = Guid.NewGuid(),
                    Code = code,
                    Name = name,
                    Description = desc,
                    NumberOfCredits = credits,
                    Level = level,
                    Department = d
                };

                _db.Courses.Add(course);

                try
                {
                    _db.SaveChanges();

                    return course;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public Course CreateCourse(string code, string name, string desc, int credits, ProgramLevel level, Guid departmentId)
        {
            var department = _db.Departments.FirstOrDefault(x => x.Id == departmentId);

            if (department != null)
            {
                return CreateCourse(code, name, desc, credits, level, department);
            }

            return null;
        }

        public bool UpdateCourse(Guid id, string name, string desc, int credits, ProgramLevel level)
        {
            if (ValidateCourseInput(name, desc, credits, level))
            {
                var course = _db.Courses.FirstOrDefault(x => x.Id == id);
                if (course != null)
                {
                    course.Name = name;
                    course.Description = desc;
                    course.NumberOfCredits = credits;
                    course.Level = level;

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

        public Course FindById(Guid id)
        {
            return _db.Courses.Find(id);
        }

        public Course FindByCode(string code)
        {
            return _db.Courses.FirstOrDefault(x => x.Code == code);
        }

        public bool AssignSemester(Guid courseId, Guid classroomId, Guid userId, decimal cost, Term term, DateTime courseDate)
        {
            if (ValidateSemesterCourseInput(cost, term, courseDate))
            {
                var course = FindById(courseId);
                var teacher = _db.Teachers.FirstOrDefault(x => x.ApplicationUserId == userId);
                if (course != null && teacher != null && !SemesterCourseExists(course.Id, term))
                {
                    var classroom = _db.Classrooms.FirstOrDefault(x => x.Id == classroomId);

                    if (classroom != null)
                    {
                        _db.SemesterCourses.Add(new SemesterCourse
                        {
                            Id = Guid.NewGuid(),
                            Classroom = classroom,
                            Course = course,
                            Term = term,
                            CourseDate = courseDate,
                            Teacher = teacher
                        });

                        try
                        {
                            _db.SaveChanges();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            return false;
        }

        public bool AssignStudentGrade(Guid studentCourseId, string grade)
        {
            if (!string.IsNullOrEmpty(grade))
            {
                var studentCourse = _db.StudentCourses.FirstOrDefault(x => x.Id == studentCourseId);
                if (studentCourse != null)
                {
                    studentCourse.Grade = grade;

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

        private bool SemesterCourseExists(Guid courseId, Term term)
        {
            return _db.SemesterCourses.Any(x => x.Course.Id == courseId && x.Term == term && x.CourseDate.Year == DateTime.Now.Year);
        }

        private bool IsCourseExists(string code)
        {
            return _db.Courses.Any(x => x.Code == code);
        }

        private bool ValidateSemesterCourseInput(decimal cost, Term term, DateTime date)
        {
            if (cost <= 0 ||
                (int)term == 0 ||
                date < DateTime.Now.AddDays(-1))
                return false;
            return true;
        }

        private bool ValidateCourseInput(string name, string desc, int credits, ProgramLevel level)
        {
            if (string.IsNullOrEmpty(name) ||
                name.Length <= 3 ||
                credits <= 0 ||
                (int)level == 0)
                return false;
            return true;
        }
    }
}
