using StudentInfo.Data;
using StudentInfo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.CourseManager
{
    public class ClassroomService
    {
        private StudentInfoContext _db;

        public ClassroomService()
        {
            _db = new StudentInfoContext();
        }

        public Guid CreateClassroom(string campus, string number, int capacity)
        {
            if (!ClassroomExists(number, campus))
            {
                var classroom = new Classroom
                {
                    Id = Guid.NewGuid(),
                    Campus = campus,
                    Number = number,
                    Capacity = capacity
                };

                _db.Classrooms.Add(classroom);

                try
                {
                    _db.SaveChanges();
                    return classroom.Id;
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
            return FindByNumber(number, campus).Id;
        }

        public Classroom FindById(Guid id)
        {
            return _db.Classrooms.Find(id);
        }

        public Classroom FindByNumber(string number, string campus)
        {
            return _db.Classrooms.FirstOrDefault(x => x.Number == number && x.Campus == campus);
        }

        public bool IsClassroomAvailable(Guid classroomId, DateTime startTime, DateTime endTime)
        {
            var classroom = _db.Classrooms.Find(classroomId);
            if (classroom != null)
            {
                if (classroom.ClassroomCourses.Any(x => endTime.Subtract(x.TimeSlotTo).Hours < 3))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public bool ReserveClassroom(Guid classroomId, DateTime startTime, DateTime endTime, out Guid classroomCourseId)
        {
            if (IsClassroomAvailable(classroomId, startTime, endTime))
            {
                var classroom = _db.Classrooms.Find(classroomId);
                if (classroom != null)
                {
                    var classroomCourse = new ClassroomCourse
                    {
                        Id = Guid.NewGuid(),
                        Classroom = classroom,
                        TimeSlotFrom = startTime,
                        TimeSlotTo = endTime
                    };

                    classroom.ClassroomCourses.Add(classroomCourse);

                    try
                    {
                        _db.SaveChanges();

                        classroomCourseId = classroomCourse.Id;
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            classroomCourseId = new Guid();
            return false;
        }

        private bool ClassroomExists(string number, string campus)
        {
            return _db.Classrooms.Any(x => x.Campus == campus && x.Number == number);
        }
    }
}
