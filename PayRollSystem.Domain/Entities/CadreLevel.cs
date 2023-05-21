using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Entities
{
    public class CadreLevel
    {
        [Key]
        public string CadreLevelId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public Double Salary { get; set; }

        public List<Employee> Employees { get; set; }
        public List<PayrollStructure> PayrollStructures { get; set; }
    }

}
