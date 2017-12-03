using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Users.Dto;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace StudentInfo.CourseManager
{
    public class DepartmentService
    {
        private StudentInfoContext _db;

        public DepartmentService()
        {
            _db = new StudentInfoContext();
        }

        public Department CreateDepartment(string name, Faculty faculty)
        {
            if (!string.IsNullOrEmpty(name) && !(IsDepartmentExists(name)))
            {
                var f = _db.Faculties.FirstOrDefault(x => x.Id == faculty.Id);
                var department = new Department
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Faculty = f
                };
                _db.Departments.Add(department);

                try
                {
                    _db.SaveChanges();
                    return department;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        private bool IsDepartmentExists(string name)
        {
            return _db.Departments.Any(x => x.Name == name);
        }
    }
}
