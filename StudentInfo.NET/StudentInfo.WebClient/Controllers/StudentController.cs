﻿using StudentInfo.Enums;
using StudentInfo.WebClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudentInfo.Data;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize]
    public class StudentController : Controller
    {
        private Students.StudentService _studentService;

        public StudentController()
        {
            _studentService = new Students.StudentService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult Grades()
        {
            if (User.Identity.IsAuthenticated)
            {
                var student = _studentService.FindByUserId(User.Identity.GetUserId());
                if (student != null)
                {

                }
            }
            return HttpNotFound();
        }

        [AuthorizeRoles(SystemRoles.Student)]
        public ActionResult Transcript()
        {
            return HttpNotFound();
        }
    }
}