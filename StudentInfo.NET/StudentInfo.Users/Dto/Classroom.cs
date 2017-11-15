using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfo.Users.Dto
{
    public class Classroom
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Campus { get; set; }
        public int Capacity { get; set; }
    }
}
