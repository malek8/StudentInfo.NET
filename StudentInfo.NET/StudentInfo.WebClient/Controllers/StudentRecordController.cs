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
using StudentInfo.Data;

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
            var studentId = HttpContext.Session["studentId"].ToString();
            var parsedStudentId = Guid.Parse(studentId);

            return View(BuildStudentRecordModel(parsedStudentId));
        }

        public ActionResult Show(Guid studentId)
        {
            var model = BuildStudentRecordModel(studentId);

            return View("Index", model);
        }

        private StudentRecordModel BuildStudentRecordModel(Guid studentId)
        {
            var model = new StudentRecordModel();

            var db = new StudentInfoContext();
            var student = db.Students.Find(studentId);

            if (student != null)
            {
                var years = student.StudentCourses.Select(x => x.SemesterCourse.CourseDate.Year).Distinct();

                var groupedYears = new Dictionary<int, IEnumerable<IGrouping<Term, StudentCourse>>>();
                foreach (var year in years)
                {
                    var group = student.StudentCourses.Where(x => x.SemesterCourse.CourseDate.Year == year)
                        .GroupBy(x => x.SemesterCourse.Term);

                    groupedYears.Add(year, group);
                }

                model.Student = student;
                model.GroupedStudentCourses = groupedYears;
                model.CummulativeGPA = StudentHelper.CummulativeGPA(student.StudentCourses.ToList());
                model.TotalEarnedCredits = StudentHelper.TotalEarnedCredits(student.StudentCourses.ToList());
            }
            return model;
        }
    }
}