using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayRollSystem.Domain.DTO;
using PayRollSystem.Domain.IServices;

namespace PayRollSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("All-Employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _adminService.GetEmployees();
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPut("Assign-cadrelevel")]
        public async Task<IActionResult> AssignCadrel(string Email, string CadrelName)
        {
            var result = await _adminService.AssignCadreToEmployee(Email, CadrelName);
            return result.Succeeded ? Ok(result): BadRequest(result);
        }
        [HttpPut("Change-Position")]
        public async Task<IActionResult> ChangeEmployeePosition(string Email,string NewPosition)
        {
            var result = await _adminService.ChangeEmployeePosition(Email, NewPosition);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Cadrelevel")]
        public async Task<IActionResult> CreateCadreLevel(string Name, double salary)
        {
            _adminService.CreateCadreLevel(Name, salary);
            return Ok("Done");
        }
        [HttpPost("Earnings")]
        public async Task<IActionResult> AddEarningComponent(string EmployeeMail, string name, double amount, string PayrollStructureId)
        {
            var result = await _adminService.AddEarningComponent(EmployeeMail, name, amount, PayrollStructureId);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Deductions")]
        public async Task<IActionResult> AddDeductionComponent(string EmployeeMail, string name, double amount, string PayrollStructureId)
        {
            var result = await _adminService.AddDeductionComponent(EmployeeMail, name, amount, PayrollStructureId);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPost("create-Paystructure")]
        public async Task<IActionResult> CreatePayStructure(CreatePaystructureDTO model)
        {
            var result = await _adminService.CreatePayRollStructure(model);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
        [HttpPost("create-Position")]
        public async Task<IActionResult> CreatePosition(string PositionTitle)
        {
            var result = await _adminService.CreatePosition(PositionTitle);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}
