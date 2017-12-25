using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentInfo.Students;
using StudentInfo.WebClient.App_Start;
using StudentInfo.WebClient.Models;
using StudentInfo.Enums;
using StudentInfo.Dto;

namespace StudentInfo.WebClient.Controllers
{
    public class StudentRecordController : Controller
    {
        private StudentService _studentService;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public StudentRecordController()
        {
            _studentService = new StudentService();
        }

        public ActionResult Index()
        {
            if (HttpContext.Session["studentId"] == null ||
                HttpContext.Session["emailAddress"] == null)
            {
                return HttpNotFound();
            }

            var model = new StudentRecordModel();
            var emailAddress = HttpContext.Session["emailAddress"].ToString();
            var user = UserManager.FindByEmail(emailAddress);

            if (user != null)
            {
                var student = _studentService.FindByUserId(user.Id);

                model.User = user;
                model.Student = student;

                var years = student.StudentCourses.Select(x => x.SemesterCourse.CourseDate.Year).Distinct();

                var groupedYears = new Dictionary<int, IEnumerable<IGrouping<Term, StudentCourse>>>();
                foreach (var year in years)
                {
                    var group = student.StudentCourses.Where(x => x.SemesterCourse.CourseDate.Year == year)
                        .GroupBy(x => x.SemesterCourse.Term);

                    groupedYears.Add(year, group);
                }

                model.GroupedStudentCourses = groupedYears;
            }

            return View(model);
        }
    }
}