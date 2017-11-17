using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentInfo.Enums;
using System.Web.Mvc;

namespace StudentInfo.WebClient.Helpers
{
    public class Helper
    {
        public static Term CurrentTerm()
        {
            var currentMonth = DateTime.Now.Month;

            switch(currentMonth)
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
    }
}