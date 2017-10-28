using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentInfo.WebClient.Controllers
{
    public class StudentCenterController : Controller
    {
        // GET: StudentCenter
        public ActionResult Index()
        {
            return View();
        }
    }
}