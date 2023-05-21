using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.DTO
{
    public class PayRollComponentDTO
    {
        public string Name { get; set; }
        public Double Amount { get; set; }
        public bool IsEarning { get; set; }
    }
}
