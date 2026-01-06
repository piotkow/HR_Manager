using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.Interfaces;
using HRManager.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPhotoService _photoService;

        public EmployeeController(IEmployeeService employeeService, IPhotoService photoService)
        {
            _employeeService = employeeService;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return employees;
        }

        [HttpGet("byTeam/{teamId}")]
        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetEmployeesByTeamId(int teamId)
        {
            var employees = await _employeeService.GetEmployeeByTeamIdAsync(teamId);
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePositionTeamResponse>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeRequest employeeReq)
        {
            var insertedEmployee = await _employeeService.InsertEmployeeAsync(employeeReq);
            return CreatedAtAction("GetEmployeeById", new { id = insertedEmployee.EmployeeID }, insertedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeRequest employeeReq)
        {
            await _employeeService.UpdateEmployeeAsync(id, employeeReq);
            return Ok(employeeReq);
        }
    }

}
