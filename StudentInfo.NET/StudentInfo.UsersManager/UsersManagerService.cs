using StudentInfo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data.UserDbContext;
using Microsoft.AspNet.Identity.EntityFramework;
using StudentInfo.Dto;
using StudentInfo.Enums;
using Microsoft.AspNet.Identity;

namespace StudentInfo.UsersManager
{
    public class UsersManagerService
    {
        private static UserDbContext _userContext = UserDbContext.Create();
        private static UserStore<ApplicationUser> _userStore = new UserStore<ApplicationUser>(_userContext);
        private UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(_userStore);

        public ApplicationUser FindByEmail(string emailAddress)
        {
            return _userManager.FindByEmail(emailAddress);
        }

        public void AssignRole(string emailAddress, string role)
        {
            var user = FindByEmail(emailAddress);
            if (user != null)
            {
                _userManager.AddToRole(user.Id, role);
            }
        }
    }
}
