using PayRollSystem.Domain.DTO;
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
    public class AdminService: IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task<Response<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            return await _adminRepository.GetEmployees();
        }

        public Task<Response<string>> AssignCadreToEmployee(string EmployeeMail, string cadreName)
        {
            return _adminRepository.AssignCadreToEmployee(EmployeeMail, cadreName);
        }

        public async Task<Response<string>> ChangeEmployeePosition(string employMail, string newPosition)
        {
            return await _adminRepository.ChangeEmployeePosition(employMail, newPosition);
        }

        public void CreateCadreLevel(string name, double salary)
        {
            _adminRepository.CreateCadreLevel(name, salary);
        }

        public async Task<Response<string>> AddEarningComponent(string EmployeeMail, string name, double amount, string PayrollStructureId)
        {
            return await _adminRepository.AddEarningComponent(EmployeeMail, name, amount, PayrollStructureId);
        }

        public async Task<Response<string>> AddDeductionComponent(string EmployeeMail, string name, double amount, string PayrollStructureId)
        {
            return await _adminRepository.AddDeductionComponent(EmployeeMail, name, amount, PayrollStructureId);
        }

        public async Task<Response<string>> CreatePayRollStructure(CreatePaystructureDTO model)
        {
            return await _adminRepository.CreatePayRollStructure(model);
        }

        public async Task<Response<string>> CreatePosition(string PositionTitle)
        {
            return await _adminRepository.CreatePosition(PositionTitle);
        }
    }
}
