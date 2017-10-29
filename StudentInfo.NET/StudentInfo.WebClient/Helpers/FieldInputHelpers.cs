using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInfo.Data;

namespace StudentInfo.WebClient.Helpers
{
    public static class FieldInputHelpers
    {
        public static IEnumerable<SelectListItem> GetSchoolDepartments(Guid? facultyId = null)
        {
            var db = new StudentInfoContext();

            var departments = db.Departments.AsQueryable();

            if (facultyId != null)
            {
                departments = departments.Where(x => x.Faculty.Id == facultyId);
            }

            return departments.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public static IEnumerable<SelectListItem> GetSchoolFaculties()
        {
            var db = new StudentInfoContext();

            return db.Faculties.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }
    }
}