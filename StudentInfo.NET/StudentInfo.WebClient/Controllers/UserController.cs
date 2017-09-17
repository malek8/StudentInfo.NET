using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StudentInfo.Users.Dto;
using StudentInfo.Data;

namespace StudentInfo.WebClient.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

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

            var users = db.ApplicationUsers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.LastName.Contains(searchString) ||
                x.FirstName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(x => x.LastName);
                    break;
                default:
                    users = users.OrderBy(x => x.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
    }
}