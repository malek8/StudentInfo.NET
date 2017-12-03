using StudentInfo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.Data;
using StudentInfo.Enums;

namespace StudentInfo.Users
{
    public class UserSearch
    {
        private StudentInfoContext _db;

        public UserSearch()
        {
            _db = new StudentInfoContext();
        }

        public IEnumerable<ApplicationUser> Search(Filters filters)
        {
            var users = _db.ApplicationUsers.AsQueryable();

            if (!string.IsNullOrEmpty(filters.Keyword))
            {
                users = users.Where(x => x.LastName.Contains(filters.Keyword) ||
                x.FirstName.Contains(filters.Keyword) ||
                x.MiddleName.Contains(filters.Keyword));
            }

            switch(filters.SortBy)
            {
                case UserSearchConstants.FirstName:
                    if (filters.SortDirection == SearchConstants.Ascending) users = users.OrderBy(x => x.FirstName);
                    else users = users.OrderByDescending(x => x.FirstName);
                    break;
                default:
                case UserSearchConstants.LastName:
                    if (filters.SortDirection == SearchConstants.Ascending) users = users.OrderBy(x => x.LastName);
                    else users = users.OrderByDescending(x => x.LastName);
                    break;
            }

            return users;
        }
    }
}
