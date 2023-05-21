using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Entities
{
    public class PayrollStructure
    {
        [Key]
        public string PayrollStructureId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("CadreLevelId")]
        public CadreLevel CadreLevel { get; set; }

       [ForeignKey("PositionId")]
        public Position Position { get; set; }

        public ICollection<PayrollComponent> Components { get; set; }
       
    }

}
