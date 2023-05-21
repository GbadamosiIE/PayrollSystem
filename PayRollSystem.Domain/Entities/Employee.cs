using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.Entities
{
    public class Employee: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        [ForeignKey("CadreLevelId")]
        public CadreLevel? CadreLevel { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }

        public string Gender { get; set; }
        [ForeignKey("PositionId")]
        public Position? Position { get; set; }
        public bool IsDeleted { get; set; }
    }

}
