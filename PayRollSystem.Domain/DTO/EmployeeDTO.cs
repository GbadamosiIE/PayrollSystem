using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.DTO
{
    public class EmployeeDTO
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
    }
}
