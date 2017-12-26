using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Enums
{
    public class Grade
    {
        public const string APlus = "A+";
        public const string A = "A";
        public const string AMinus = "A-";

        public const string BPlus = "B+";
        public const string B = "B";
        public const string BMinus = "B-";

        public const string CPlus = "C+";
        public const string C = "C";
        public const string CMinus = "C-";

        public const string DPlus = "D+";
        public const string D = "D";
        public const string DMinus = "D-";

        public const string F = "F";

        public static decimal GetAverage(string grade)
        {
            switch(grade)
            {
                case "A+":
                    return 4.30m;
                case "A":
                    return 4.00m;
                case "A-":
                    return 3.70m;
                case "B+":
                    return 3.30m;
                case "B":
                    return 3.00m;
                case "B-":
                    return 2.70m;
                case "C+":
                    return 2.30m;
                case "C":
                    return 2.00m;
                case "C-":
                    return 1.70m;
                case "D+":
                    return 1.30m;
                case "D":
                    return 1.00m;
                case "D-":
                    return 0.70m;
                default:
                    return 0;
            }
        }
    }
}
