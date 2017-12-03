using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Dto;

namespace StudentInfo.CourseManager
{
    public class FacultyService
    {
        private StudentInfoContext _db;

        public FacultyService()
        {
            _db = new StudentInfoContext();
        }

        public Faculty CreateFaculty(string name)
        {
            if (!string.IsNullOrEmpty(name) && !FacultyExists(name))
            {
                var faculty = new Faculty
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };

                _db.Faculties.Add(faculty);

                try
                {
                    _db.SaveChanges();

                    return faculty;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }

        private bool FacultyExists(string name)
        {
            return _db.Faculties.Any(x => x.Name == name);
        }
    }
}
