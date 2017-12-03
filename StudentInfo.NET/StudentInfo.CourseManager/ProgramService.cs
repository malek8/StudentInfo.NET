using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Enums;
using StudentInfo.Users.Dto;

namespace StudentInfo.CourseManager
{
    public class ProgramService
    {
        private StudentInfoContext _db;

        public ProgramService()
        {
            _db = new StudentInfoContext();
        }

        public Guid CreateProgram(Department department, string name, ProgramLevel level, DegreeType degree, ProgramStudyType type)
        {
            if (!string.IsNullOrEmpty(name) && !(IsProgramExists(name)))
            {
                var d = _db.Departments.FirstOrDefault(x => x.Id == department.Id);
                var program = new Program
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Level = level,
                    DegreeType = degree,
                    StudyType = type,
                    Department = d
                };

                _db.Programs.Add(program);

                try
                {
                    _db.SaveChanges();
                    return program.Id;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Guid.Empty;
        }

        private bool IsProgramExists(string name)
        {
            return _db.Programs.Any(x => x.Name == name);
        }
    }
}
