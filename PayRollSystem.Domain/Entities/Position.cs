using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Entities
{
    public class Position
    {
        [Key]
        public string PositionId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        // Other position properties

        public List<Employee> Employees { get; set; }
        public List<PayrollStructure> PayrollStructures { get; set; }
    }

}
