using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayRollSystem.Domain.Entities;

namespace PayRollSystem.Data.Context
{
    public class PayRollSystemContext: IdentityDbContext<Employee>
    {
       


   
        public PayRollSystemContext(DbContextOptions<PayRollSystemContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<CadreLevel> CadreLevels { get; set; }
        public DbSet<PayrollComponent> PayrollComponents { get; set; }
        public DbSet<PayrollStructure> PayrollStructures { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}

