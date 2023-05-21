using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.Entities;
using PayRollSystem.Domain.IServices;
using PayRollSystem.Domain.Utilities;

namespace PayRollSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

       
        [HttpPut("Update-Employee")]
        public async Task<IActionResult> UpdateEmployeeInformation(EmployeeDTO model)
        {
            var result = await _employeeService.UpdateEmployee(model);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPatch("Delete-Employee")]
        public async Task<IActionResult> SoftDeleteEmployeeInformation()
        {
            var result = await _employeeService.DeleteEmployee();
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpGet("CalculateEarnings")]
        public async Task<IActionResult> CalculateNetEarnings()
        {
            var result = await _employeeService.CalculateNetEarnings();
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpGet("Check-PayStructure")]
        public async Task<IActionResult> CheckPayStructure()
        {
            var result = await _employeeService.CalculateNetEarnings();
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}
