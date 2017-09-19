using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using StudentInfo.Enums;
using StudentInfo.Users.Dto;

namespace StudentInfo.Data
{
    public static class CustomSeed
    {
        public static void AssignUserRoles()
        {
            var context = UserDbContext.UserDbContext.Create();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = userManager.FindByEmail("malek.atwiz@hotmail.com");
            userManager.AddToRole(user.Id, SystemRoles.Administrator);
        }
    }
}
