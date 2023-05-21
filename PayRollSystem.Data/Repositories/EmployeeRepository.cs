using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PayRollSystem.Data.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly PayRollSystemContext _context;
        private readonly ITokenDetails _tokenDetails;
        private readonly IMapper _mapper;

        public EmployeeRepository(PayRollSystemContext context, ITokenDetails tokenDetails, IMapper mapper)
        {
            _context = context;
            _tokenDetails = tokenDetails;
            _mapper = mapper;
        }
        //GET /employees: Retrieve a list of all employees.

        
        public async Task<Response<string>> UpdateEmployee(EmployeeDTO employee)
        {
            var employeeId = _tokenDetails.GetId();
            var currentEmployee = await _context.Employees.Where(x=> x.Id == employeeId).FirstOrDefaultAsync();
            if(currentEmployee is not null)
            {
                currentEmployee.LastName = employee.LastName;
                currentEmployee.FirstName = employee.FirstName;
                currentEmployee.Phone = employee.Phone;
                currentEmployee.Gender = employee.Gender;
                currentEmployee.Age = employee.Age;

                _context.Employees.Update(currentEmployee);
                await _context.SaveChangesAsync();
                return Response<string>.Success("Sucessfully Update Employee", employeeId);
            }
            return Response<string>.Fail("User not logged in");
        }
        public async Task<Response<string>> DeleteEmployee()
        {
            var employeeId = _tokenDetails.GetId();
            var currentEmployee = await _context.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
            if (currentEmployee is not null)
            {
                currentEmployee.IsDeleted = true;

                _context.Employees.Update(currentEmployee);
                await _context.SaveChangesAsync();
                return Response<string>.Success("Sucessfully Delete", employeeId);
            }
            return Response<string>.Fail("User not logged in");
        }
        public async Task<Response<Double>> CalculateNetEarnings()
        {
           
            var employeeId = _tokenDetails.GetId();
          
            var employee = await _context.Employees.Include(e => e.CadreLevel)
                                              .Include(e => e.Position)
                                              .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return Response<Double>.Fail("Employee does not exit");
            }
            var payrollStructure = await _context.PayrollStructures.Include(ps => ps.Components)
                                                             .FirstOrDefaultAsync(ps => ps.CadreLevel.CadreLevelId == employee.CadreLevel.CadreLevelId
                                                             && ps.Position.PositionId == employee.Position.PositionId);

            if (payrollStructure == null)
            {
                return Response<Double>.Fail("No payroll structure for this employee yet");
            }

            Double totalEarnings = payrollStructure.Components.Where(x=> x.IsEarning == true).Sum(a => a.Amount);

            Double netEarnings = totalEarnings - payrollStructure.Components.Where(x=> x.IsEarning == false).Sum(d => d.Amount);

            return Response<Double>.Success("Your NetEarning is", netEarnings);
        }
        //public async Task<Response<PayrollStructure>> CheckPaystructure()
        //{
        //    var employeeId = _tokenDetails.GetId();
        //    var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == employeeId);
        //    var paystructure = await _context.PayrollStructures
        //        .FirstOrDefaultAsync(x=> x.CadreLevel.CadreLevelId == employee.CadreLevel.CadreLevelId 
        //        && x.Position.PositionId == employee.Position.PositionId);
        //    if(paystructure != null)
        //    {
        //        Response<PayrollStructure>.Success("Successful", paystructure);
        //    }
        //    return Response<PayrollStructure>.Fail("Operation Fail");
        //}
        //public async Task<Response<PayrollStructure>> CheckPaystructure()
        //{
        //    var employeeId = _tokenDetails.GetId();
        //    var employee = await _context.Employees
        //        .Where(x => x.Id == employeeId)
        //        .Join(_context.PayrollStructures,
        //            e => new { e.CadreLevel.CadreLevelId, e.Position.PositionId },
        //            ps => new { CadreLevelId = ps.CadreLevel.CadreLevelId, PositionId = ps.Position.PositionId },
        //            (e, ps) => new { Employee = e, PayrollStructure = ps })
        //        .FirstOrDefaultAsync();

        //    if (employee?.PayrollStructure != null)
        //    {
        //        return Response<PayrollStructure>.Success("Successful", employee.PayrollStructure);
        //    }

        //    return Response<PayrollStructure>.Fail("Operation Fail");
        //}
        public async Task<Response<PayrollStructure>> CheckPaystructure()
        {
            var employeeId = _tokenDetails.GetId();
            var employee = await _context.Employees
                .Include(e => e.CadreLevel)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(x => x.Id == employeeId);

            if (employee != null)
            {
                var paystructure = await _context.PayrollStructures
                    .Include(ps => ps.CadreLevel)
                    .Include(ps => ps.Position)
                    .FirstOrDefaultAsync(x => x.CadreLevel.CadreLevelId == employee.CadreLevel.CadreLevelId
                                              && x.Position.PositionId == employee.Position.PositionId);

                if (paystructure != null)
                {
                    return Response<PayrollStructure>.Success("Successful", paystructure);
                }
            }

            return Response<PayrollStructure>.Fail("Operation Fail");
        }


    }

}
