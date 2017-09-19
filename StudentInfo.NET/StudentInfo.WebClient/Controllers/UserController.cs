using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StudentInfo.Users.Dto;
using StudentInfo.Data;
using StudentInfo.Enums;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize(Roles =SystemRoles.Administrator)]
    public class UserController : Controller
    {
        public ActionResult Index(string sortBy, string sortDirection, string currentFilter, string searchString, int? page)
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

            var users = db.ApplicationUsers.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.LastName.Contains(searchString) ||
                x.FirstName.Contains(searchString));
            }

            switch(sortBy)
            {
                case UserSearchConstants.FirstName:
                    if (sortDirection == SearchConstants.Ascending) users = users.OrderBy(x => x.FirstName);
                    else users = users.OrderByDescending(x => x.FirstName);
                    break;
                default:
                case UserSearchConstants.LastName:
                    if (sortDirection == SearchConstants.Ascending) users = users.OrderBy(x => x.LastName);
                    else users = users.OrderByDescending(x => x.LastName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
    }
}