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

namespace StudentInfo.WebClient.Controllers
{
    public class ManageStudentsController : Controller
    {
        [Authorize]
        [AuthorizeRoles(SystemRoles.Administrator, SystemRoles.FacultyMember, SystemRoles.Advisor)]
        public ActionResult Search(StudentSearchModel model, int? page)
        {
            if (model == null) model = new StudentSearchModel();

            var db = new StudentInfoContext();

            var students = db.Students.ToList();

            if (!string.IsNullOrEmpty(model.Name))
            {
                
            }

            return View();
        }
    }
}