using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using StudentInfo.Users.Dto;

namespace StudentInfo.Data.UserDbContext
{
    public class UserDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserDbContext()
            :base("DefaultConnection")
        {
            Database.SetInitializer<UserDbContext>(null);
        }

        public static UserDbContext Create()
        {
            return new UserDbContext();
        }
    }
}
