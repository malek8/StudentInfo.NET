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
        private UserSearch _userSearch;
        private UserService _userService;

        public UserController()
        {
            _userSearch = new UserSearch();
            _userService = new UserService();
        }

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

            var results = _userSearch.Search(filters);

            int pageNumber = (page ?? 1);
            return View(results.ToPagedList(pageNumber, SearchConstants.PageSize));
        }

        public ActionResult Details(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.FindById(id);

                if (user != null)
                {
                    return View("_UserDetails", user);
                }
            }

            return HttpNotFound();
        }

        public JsonResult Edit(ApplicationUser user)
        {
            if (user != null)
            {
                var result = _userService.Edit(user);

                if (result)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false, message = "Something wrong happened" });
        }
    }
}