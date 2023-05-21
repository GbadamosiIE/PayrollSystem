using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Entities;
using PayRollSystem.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.IServices
{
    public interface IEmployeeService
    {
       
        Task<Response<string>> UpdateEmployee(EmployeeDTO employee);
        Task<Response<string>> DeleteEmployee();
        Task<Response<Double>> CalculateNetEarnings();
        Task<Response<PayrollStructure>> CheckPaystructure();
    }
}
