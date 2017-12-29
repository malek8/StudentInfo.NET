using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient.Models
{
    public class CalendarObject
    {
        public int success { get; set; }
        public List<CalendarItem> result { get; set; }
    }

    public class CalendarItem
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string @class { get; set; }
        public long start { get; set; }
        public long end { get; set; }
    }
}