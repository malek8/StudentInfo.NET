using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Enums;
using System.Web.Mvc;
using StudentInfo.Users.Dto;
using StudentInfo.Data;

namespace StudentInfo.WebClient.Helpers
{
  public class Helper
  {
    public static Term CurrentTerm()
    {
      var currentMonth = DateTime.Now.Month;

      switch (currentMonth)
      {
        case 1:
        case 2:
        case 3:
        case 4:
          return Term.Winter;
        case 5:
        case 6:
          return Term.Summer1;
        case 7:
        case 8:
          return Term.Summer2;
        case 9:
        case 10:
        case 11:
        case 12:
          return Term.Fall;
        default:
          return Term.Fall;
      }
    }

    public static JsonResult CreateResponse(bool success, string message)
    {
      return new JsonResult()
      {
        Data = new
        {
          success = success,
          message = message
        },
        JsonRequestBehavior = JsonRequestBehavior.AllowGet
      };
    }

    public static string GetUserFullName(Guid userId)
    {
      var context = new StudentInfo.Data.StudentInfoContext();

      var user = context.ApplicationUsers.FirstOrDefault(x => x.Id == userId.ToString());

      if (user != null)
      {
        return $"{user.FirstName} {user.LastName}";
      }

      return string.Empty;
    }

    public static string GetUserFirstName(Guid userId)
    {
      var context = new StudentInfo.Data.StudentInfoContext();

      var user = context.ApplicationUsers.FirstOrDefault(x => x.Id == userId.ToString());

      if (user != null)
      {
        return user.FirstName;
      }

      return string.Empty;
    }

    public static string GetUserLastName(Guid userId)
    {
      var context = new StudentInfo.Data.StudentInfoContext();

      var user = context.ApplicationUsers.FirstOrDefault(x => x.Id == userId.ToString());

      if (user != null)
      {
        return user.LastName;
      }

      return string.Empty;
    }

    public static IEnumerable<SelectListItem> GetTeachers()
    {
      var db = new StudentInfoContext();

      var teachers = db.ApplicationUsers;
      // TO DO: Filter by Role to get teachers.

      return teachers.Select(x => new SelectListItem
      {
        Text = x.FirstName + " " + x.LastName,
        Value = x.Id
      });
    }

    public static IEnumerable<SelectListItem> GetGradeItems()
    {
      return new List<SelectListItem>
      {
        new SelectListItem()
        {
          Text = Grade.APlus,
          Value = Grade.APlus
        },
        new SelectListItem()
        {
          Text = Grade.A,
          Value = Grade.A
        },
        new SelectListItem()
        {
          Text = Grade.AMinus,
          Value = Grade.AMinus
        },
        new SelectListItem()
        {
          Text = Grade.BPlus,
          Value = Grade.BPlus
        },
        new SelectListItem()
        {
          Text = Grade.B,
          Value = Grade.B
        },
        new SelectListItem()
        {
          Text = Grade.BMinus,
          Value = Grade.BMinus
        },
        new SelectListItem()
        {
          Text = Grade.CPlus,
          Value = Grade.CPlus
        },
        new SelectListItem()
        {
          Text = Grade.C,
          Value = Grade.C
        },
        new SelectListItem()
        {
          Text = Grade.CMinus,
          Value = Grade.CMinus
        },
        new SelectListItem()
        {
          Text = Grade.DPlus,
          Value = Grade.DPlus
        },
        new SelectListItem()
        {
          Text = Grade.D,
          Value = Grade.D
        },
        new SelectListItem()
        {
          Text = Grade.DMinus,
          Value = Grade.DMinus
        },
        new SelectListItem()
        {
          Text = Grade.F,
          Value = Grade.F
        }
      };
    }
  }
}