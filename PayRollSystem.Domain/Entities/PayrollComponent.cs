using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Entities
{
    public class PayrollComponent
    {
        public string PayrollComponentId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public Double Amount { get; set; }
        public bool IsEarning { get; set; }

        [ForeignKey("PayrollStructureId")]
        public PayrollStructure PayrollStructure { get; set; }
    }

}
