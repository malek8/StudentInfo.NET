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

        public Course FindById(Guid id)
        {
            return _db.Courses.Find(id);
        }

        public Course FindByCode(string code)
        {
            return _db.Courses.FirstOrDefault(x => x.Code == code);
        }

        public bool AssignSemester(Guid courseId, Guid classroomId, decimal cost, Term term, DateTime courseDate)
        {
            if (ValidateCourseInput(cost, term, courseDate))
            {
                var course = FindById(courseId);
                if (course != null && !SemesterCourseExists(course.Id, term))
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
                            Cost = cost,
                            CourseDate = courseDate
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

        private bool SemesterCourseExists(Guid courseId, Term term)
        {
            return _db.SemesterCourses.Any(x => x.Course.Id == courseId && x.Term == term && x.CourseDate.Year == DateTime.Now.Year);
        }

        private bool IsCourseExists(string code)
        {
            return _db.Courses.Any(x => x.Code == code);
        }

        private bool ValidateCourseInput(decimal cost, Term term, DateTime date)
        {
            if (cost <= 0 ||
                (int)term == 0 ||
                date < DateTime.Now.AddDays(-1))
                return false;
            return true;
        }
    }
}
