using StudentInfo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;

namespace StudentInfo.Users
{
    public class UserService
    {
        private StudentInfoContext _db;

        public UserService()
        {
            _db = new StudentInfoContext();
        }

        public ApplicationUser FindById(string id)
        {
            return _db.ApplicationUsers.FirstOrDefault(x => x.Id == id);
        }

        public bool Edit(ApplicationUser user)
        {
            if (ValidateInfo(user))
            {
                var u = _db.ApplicationUsers.FirstOrDefault(x => x.Id == user.Id);

                if (u != null)
                {
                    u.FirstName = user.FirstName;
                    u.MiddleName = user.MiddleName;
                    u.LastName = user.LastName;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Email = user.Email;

                    try
                    {
                        _db.SaveChanges();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return false;
        }

        private bool ValidateInfo(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.FirstName) ||
                string.IsNullOrEmpty(user.LastName) ||
                string.IsNullOrEmpty(user.Email) ||
                user.FirstName.Length < 3 ||
                user.LastName.Length < 3 ||
                user.FirstName.Length > 255 ||
                user.LastName.Length > 255)
                return false;
            return true;
        }
    }
}
