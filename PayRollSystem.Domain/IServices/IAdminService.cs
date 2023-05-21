using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Domain.IServices
{
    public interface IAdminService
    {
        Task<Response<IEnumerable<EmployeeDTO>>> GetEmployees();
        Task<Response<string>> AssignCadreToEmployee(string EmployeeMail, string cadreName);
        Task<Response<string>> ChangeEmployeePosition(string employMail, string newPosition);
        void CreateCadreLevel(string name, Double salary);
        Task<Response<string>> AddEarningComponent(string EmployeeMail, string name, Double amount, string PayrollStructureId);
        Task<Response<string>> AddDeductionComponent(string EmployeeMail, string name, Double amount, string PayrollStructureId);
        Task<Response<string>> CreatePayRollStructure(CreatePaystructureDTO model);
        Task<Response<string>> CreatePosition(string PositionTitle);
    }
}
