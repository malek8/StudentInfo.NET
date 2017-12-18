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

        public bool IsClassroomAvailable(Guid classroomId, DateTime startTime, DateTime endTime, List<DateTime> dates)
        {
            var classroom = _db.Classrooms.Find(classroomId);
            if (classroom != null)
            {
                foreach (var d in dates)
                {
                    var matchClassroom = _db.ClassroomSchedules.Where(x => x.Classroom.Id == classroomId);
                    if (matchClassroom.Any())
                    {
                        var matchDates = matchClassroom.SelectMany(x => x.ScheduleItems.Where(z => z.Date.Subtract(d).Days == 0));
                        if (matchDates.Any(x => endTime.Subtract(x.EndTime).Hours < 3))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public bool ReserveClassroom(Guid classroomId, string title, DateTime startTime, DateTime endTime, List<DateTime> dates, out Guid scheduleId)
        {
            if (IsClassroomAvailable(classroomId, startTime, endTime, dates))
            {
                var classroom = _db.Classrooms.Find(classroomId);
                if (classroom != null)
                {
                    var scheduleItems = new List<ClassroomScheduleItem>();

                    foreach(var d in dates)
                    {
                        scheduleItems.Add(new ClassroomScheduleItem
                        {
                            Id = Guid.NewGuid(),
                            Date = d,
                            StartTime = startTime,
                            EndTime = endTime
                        });
                    }

                    if (scheduleItems.Count > 0)
                    {
                        var schedule = new ClassroomSchedule
                        {
                            Id = Guid.NewGuid(),
                            Classroom = classroom,
                            Title = title,
                            ScheduleItems = scheduleItems
                        };

                        _db.ClassroomSchedules.Add(schedule);

                        try
                        {
                            _db.SaveChanges();

                            scheduleId = schedule.Id;
                            return true;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            scheduleId = new Guid();
            return false;
        }

        public ClassroomSchedule GetSchedule(Guid scheduleId)
        {
            return _db.ClassroomSchedules.Find(scheduleId);
        }

        public List<ClassroomSchedule> GetUnassignedSchedules()
        {
            var assignedSchedules = _db.SemesterCourses.Select(x => x.Schedule.Id);
            return _db.ClassroomSchedules.Where(x => !assignedSchedules.Contains(x.Id)).ToList();
        }

        private bool ClassroomExists(string number, string campus)
        {
            return _db.Classrooms.Any(x => x.Campus == campus && x.Number == number);
        }
    }
}
