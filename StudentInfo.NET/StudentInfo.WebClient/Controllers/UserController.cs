using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StudentInfo.Dto;
using StudentInfo.Data;
using StudentInfo.Enums;
using StudentInfo.Users;

namespace StudentInfo.WebClient.Controllers
{
    [RequireHttps]
    [Authorize(Roles =SystemRoles.Administrator)]
    public class UserController : Controller
    {
        public ActionResult Index(string sortBy, string sortDirection, string currentFilter, string searchString, int? page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var filters = new Filters
            {
                Keyword = searchString,
                SortBy = sortBy,
                SortDirection = sortDirection
            };

            ViewBag.CurrentFilter = searchString;

            var userSearch = new UserSearch();

            var results = userSearch.Search(filters);

            int pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, SearchConstants.PageSize));
        }
    }
}