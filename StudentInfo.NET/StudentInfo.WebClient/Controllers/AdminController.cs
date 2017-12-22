using StudentInfo.Enums;
using StudentInfo.WebClient.App_Start;
using StudentInfo.WebClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInfo.WebClient.Controllers
{
    [Authorize]
    [AuthorizeRoles(SystemRoles.Administrator)]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PublishClassrooms()
        {
            AdminPublishCommands.PublishClassrooms();

            return Json(new { message = "Finished creating classrooms" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PublishCourses()
        {
            AdminPublishCommands.PublishCourses();

            return Json(new { message = "Finished creating courses" }, JsonRequestBehavior.AllowGet);
        }
    }
}