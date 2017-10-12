using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudentInfo.Enums;
using StudentInfo.Data;
using PagedList;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize(Roles = SystemRoles.Administrator)]
    public class FacultyController : Controller
    {
        public ActionResult Index(string sortDirection, string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var db = new StudentInfoContext();

            var faculties = db.Faculties.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                faculties = faculties.Where(x => x.Name.Contains(searchString));
            }

            if (sortDirection == SearchConstants.Ascending)
            {
                faculties = faculties.OrderBy(x => x.Name);
            }
            else
            {
                faculties = faculties.OrderByDescending(x => x.Name);
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(faculties.ToPagedList(pageNumber, pageSize));
        }
    }
}