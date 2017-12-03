using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;
using StudentInfo.Users.Dto;
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
            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name) && !IsCourseExists(code))
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

        public Course FindById(Guid id)
        {
            return _db.Courses.Find(id);
        }

        public Course FindByCode(string code)
        {
            return _db.Courses.FirstOrDefault(x => x.Code == code);
        }

        public bool AssignSemester(Guid classroomId, string courseCode, decimal cost, Term term)
        {
            var course = FindByCode(courseCode);
            if (course != null && !SemesterCourseExists(course.Id, term))
            {
                var classroom = _classroomService.FindById(classroomId);

                if (classroom != null)
                {
                    _db.SemesterCourses.Add(new SemesterCourse
                    {
                        Id = Guid.NewGuid(),
                        Classroom = classroom,
                        Course = course,
                        Term = term,
                        Cost = cost,
                        CourseDate = DateTime.Now
                    });

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
    }
}
