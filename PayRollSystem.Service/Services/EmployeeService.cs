using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Entities;
using PayRollSystem.Domain.IRepositories;
using PayRollSystem.Domain.IServices;
using PayRollSystem.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<Response<double>> CalculateNetEarnings()
        {
            return await _employeeRepository.CalculateNetEarnings();
        }

        public async Task<Response<PayrollStructure>> CheckPaystructure()
        {
            return await _employeeRepository.CheckPaystructure();
        }

        public async Task<Response<string>> DeleteEmployee()
        {
            return await _employeeRepository.DeleteEmployee();
        }

       

        public async Task<Response<string>> UpdateEmployee(EmployeeDTO employee)
        {
            return await _employeeRepository.UpdateEmployee(employee);
        }
    }
}
