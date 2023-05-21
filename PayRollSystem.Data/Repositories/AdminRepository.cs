using Microsoft.EntityFrameworkCore;
using PayRollSystem.Data.Context;
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Entities;
using PayRollSystem.Domain.IRepositories;
using PayRollSystem.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayRollSystem.Data.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly PayRollSystemContext _context;
        private readonly ITokenDetails _tokenDetails;
        public AdminRepository(PayRollSystemContext context, ITokenDetails tokenDetails)
        {
            _context = context;
            _tokenDetails = tokenDetails;
        }
        public async Task<Response<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var AllEmployees = await _context.Employees.Select(x => new EmployeeDTO
            {
                LastName = x.LastName,
                FirstName = x.FirstName,
                Phone = x.Phone,
                Gender = x.Gender,
                Age = x.Age,
            }).ToListAsync();
            if (AllEmployees.Any())
            {
                return Response<IEnumerable<EmployeeDTO>>.Success("List Of Employees", AllEmployees);
            }
            return Response<IEnumerable<EmployeeDTO>>.Fail("No Employ Found");
        }
        public async Task<Response<string>> AssignCadreToEmployee(string EmployeeMail, string cadreName)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == EmployeeMail);
            var cadrel = await _context.CadreLevels.FirstOrDefaultAsync(x=> x.Name == cadreName);

            if (employee == null && cadrel == null)
            {
                return Response<string>.Fail("Employee or cadrelevel Not Found");
            }

            employee.CadreLevel = cadrel;
             _context.Employees.Update(employee);

            await _context.SaveChangesAsync();
            return Response<string>.Success("success",cadreName);
        }
        public async Task<Response<string>> ChangeEmployeePosition(string employMail, string newPosition)
        {
            var query = await _context.Employees.Join(
                _context.Positions,
                employee => employee.Position.PositionId,
                position => position.PositionId,
                (employee, position) => new { Employee = employee, Position = position })
                .Where(joinResult => joinResult.Employee.Email == employMail && joinResult.Position.Title == newPosition)
                .FirstOrDefaultAsync();

            if (query == null)
            {
                return Response<string>.Fail("Operation Failed");
            }

            query.Employee.Position = query.Position;
            query.Position.Employees.Add(query.Employee);
            _context.SaveChanges();
            return Response<string>.Success("suceess", newPosition);
        }
        public void CreateCadreLevel(string name, Double salary)
        {
            var newCadreLevel = new CadreLevel
            {
                Name = name,
                Salary = salary
            };

            _context.CadreLevels.Add(newCadreLevel);
            _context.SaveChanges();
        }
        public async Task<Response<string>> AddEarningComponent(string EmployeeMail, string name, Double amount, String PayrollStructureId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == EmployeeMail);

            if (employee != null)
            {
                var earning = new PayrollComponent
                {
                    Name = name,
                    Amount = amount,
                    IsEarning = true
                };
                var structure =await _context.PayrollStructures
                    .Join(_context.CadreLevels,
                        p => p.CadreLevel.CadreLevelId,
                        c => c.CadreLevelId,
                        (p, c) => new { PayrollStructure = p, CadreLevel = c })
                    .FirstOrDefaultAsync(x => x.PayrollStructure.PayrollStructureId == PayrollStructureId
                        && x.CadreLevel.CadreLevelId == employee.CadreLevel.CadreLevelId);
                if (structure != null)
                {
                    structure.PayrollStructure.Components.Add(earning);
                    _context.SaveChanges();
                    return Response<string>.Success("Successful", name);
                }
                else
                {
                   return Response<string>.Fail("PayrollStructure not found");
                }
            }
            return Response<string>.Fail("Employee not found");
            

        }
        public async Task<Response<string>> AddDeductionComponent(string EmployeeMail, string name, Double amount, String PayrollStructureId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Email == EmployeeMail);

            if (employee != null)
            {
                var earning = new PayrollComponent
                {
                    Name = name,
                    Amount = amount,
                    IsEarning = false
                };
                var structure = await _context.PayrollStructures
                    .Join(_context.CadreLevels,
                        p => p.CadreLevel.CadreLevelId,
                        c => c.CadreLevelId,
                        (p, c) => new { PayrollStructure = p, CadreLevel = c })
                    .FirstOrDefaultAsync(x => x.PayrollStructure.PayrollStructureId == PayrollStructureId
                        && x.CadreLevel.CadreLevelId == employee.CadreLevel.CadreLevelId);
                if (structure != null)
                {
                    structure.PayrollStructure.Components.Add(earning);
                    _context.SaveChanges();
                    return Response<string>.Success("Successful", name);
                }
                else
                {
                    return Response<string>.Fail("PayrollStructure not found");
                }
            }
            return Response<string>.Fail("Employee not found");


        }
        public async Task<Response<string>> CreatePayRollStructure(CreatePaystructureDTO model)
        {
            var position = await _context.Positions.FirstOrDefaultAsync(x => x.PositionId == model.PositionId);
            var cadel = await _context.CadreLevels.FirstOrDefaultAsync(x=> x.CadreLevelId==model.CadreLevelId);
            if(cadel != null && position != null)
            {
                var paystructure = new PayrollStructure
                {
                    CadreLevel = cadel,
                    Position = position
                };
                await _context.PayrollStructures.AddAsync(paystructure);
                _context.SaveChanges();
                return Response<string>.Success("Success", paystructure.PayrollStructureId);
            }
            return Response<string>.Fail("Fail");

        }

        public async Task<Response<string>> CreatePosition(string PositionTitle)
        {
            var position = new Position
            {
                Title = PositionTitle,
            };
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return Response<string>.Success("Done", PositionTitle);
        }
       
    }

}

