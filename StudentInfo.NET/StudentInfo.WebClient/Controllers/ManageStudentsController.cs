using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInfo.Enums;
using StudentInfo.Dto;
using StudentInfo.WebClient.Helpers;
using StudentInfo.WebClient.Models;
using StudentInfo.Data;
using PagedList;
using Microsoft.AspNet.Identity;
using StudentInfo.Students;

namespace StudentInfo.WebClient.Controllers
{
    public class ManageStudentsController : Controller
    {
        private StudentService _studentService;

        public ManageStudentsController()
        {
            _studentService = new StudentService();
        }

        [Authorize]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember, SystemRoles.Advisor)]
        public ActionResult Search(StudentSearchModel model, int? page)
        {
            if (model == null) model = new StudentSearchModel();

            var db = new StudentInfoContext();
            var students = db.Students.ToList();

            if (User.IsInRole(SystemRoles.Advisor))
            {
                var userId = User.Identity.GetUserId();
                var user = db.FacultyAdvisors.FirstOrDefault(x => x.User.Id == userId);

                if (user != null)
                {
                    ViewBag.FacultyId = user.Faculty.Id;
                    students = students.Where(x => x.Program.Department.Faculty.Id == user.Faculty.Id).ToList();
                }
            }

            if (!string.IsNullOrEmpty(model.Name))
            {
                students = students.Where(x => x.User.FullName.ToLower().Contains(model.Name.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(model.StudentId))
            {
                students = students.Where(x => x.ExternalStudentId.ToString().Contains(model.StudentId)).ToList();
            }
            if (model.DepartmentId.HasValue)
            {
                students = students.Where(x => x.Program.Department.Id == model.DepartmentId.Value).ToList();
            }

            int pageNumber = (page ?? 1);

            model.Result = students.OrderBy(x => x.ExternalStudentId).
                ToPagedList(pageNumber, SearchConstants.PageSize);

            return View(model);
        }

        [Authorize]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember, SystemRoles.Advisor)]
        public ActionResult QuickSearch(Guid semesterCourseId)
        {
            var db = new StudentInfoContext();
            var model = new StudentQuickSearchModel();

            var semesterCourse = db.SemesterCourses.Find(semesterCourseId);

            var students = db.Students.Where(x => !x.StudentCourses.Any() ||
            (!x.StudentCourses.Any(z => z.SemesterCourse.Course.Id == semesterCourse.Course.Id)));

            model.Students = students.OrderBy(x => x.ExternalStudentId).ToList();
            model.SemesterCourse = semesterCourse;

            return View("_QuickSearch", model);
        }

        [Authorize]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember, SystemRoles.Advisor)]
        public ActionResult EnrollStudent(Guid studentId, Guid semesterCourseId)
        {
            var message = string.Empty;
            var result = _studentService.Enroll(studentId, semesterCourseId, out message, true);

            if (result)
            {
                return Helper.CreateResponse(true, message);
            }
            else
            {
                return Helper.CreateResponse(false, message);
            }
        }
    }
}