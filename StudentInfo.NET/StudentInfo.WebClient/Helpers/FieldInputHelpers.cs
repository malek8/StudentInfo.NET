using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInfo.Data;
using StudentInfo.Dto;

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

            var facultiesSelection = new List<SelectListItem>();

            facultiesSelection.Add(new SelectListItem()
            {
                Text = "-- Select Item --",
                Value = string.Empty
            });

            facultiesSelection.AddRange(db.Faculties.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }));

            return facultiesSelection;
        }

        public static IEnumerable<SelectListItem> GetClassrooms()
        {
            var db = new StudentInfoContext();

            var classrooms = db.Classrooms.ToList();
            var classroomsSelection = new List<SelectListItem>();

            classroomsSelection.Add(new SelectListItem()
            {
                Text = "-- Select Item --",
                Value = string.Empty
            });

            classroomsSelection.AddRange(classrooms.Select(x => new SelectListItem()
            {
                Text = $"{x.Number} / {x.Campus}",
                Value = x.Id.ToString()
            }));

            return classroomsSelection;
        }

        public static IEnumerable<SelectListItem> GetInstructors()
        {
            var selections = new List<SelectListItem>();

            selections.Add(new SelectListItem
            {
                Text = "-- Select Item --",
                Value = string.Empty
            });

            var db = new StudentInfoContext();
            var teacherIds = db.Teachers.Select(x => x.ApplicationUserId).ToList();

            var teachersInfo = new List<ApplicationUser>();

            foreach(var t in teacherIds)
            {
                var u = db.ApplicationUsers.FirstOrDefault(x => x.Id == t.ToString());
                if (u != null)
                {
                    teachersInfo.Add(u);
                }
            }

            selections.AddRange(teachersInfo.Select(x => new SelectListItem
            {
                Text = $"{x.FirstName} {x.LastName}",
                Value = x.Id.ToString()
            }));

            return selections;
        }
    }
}