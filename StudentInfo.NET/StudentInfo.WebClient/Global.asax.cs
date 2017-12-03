using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using StudentInfo.Enums;
using StudentInfo.UsersManager;

namespace StudentInfo.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            CustomSeed();
            Data.CustomSeed.AddSemesterCourses();
        }

        private void CustomSeed()
        {
            AssignUserRoles();
        }

        private void AssignUserRoles()
        {
            var userService = new UsersManagerService();

            userService.AssignRole("malek.atwiz@hotmail.com", SystemRoles.Administrator);
            userService.AssignRole("bruce.wayne@batman.com", SystemRoles.Student);
            userService.AssignRole("mr.garrison@southpark.com", SystemRoles.Instructor);
            userService.AssignRole("donald.duck@disney.com", SystemRoles.FacultyMember);
        }
    }
}
