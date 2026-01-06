using HRManager.Models.Entities;
using HRManager.Services.DTOs.DepartmentDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            return departments;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<IActionResult> InsertDepartment([FromBody] DepartmentRequest departmentReq)
        {
            var insertedDepartment = await _departmentService.InsertDepartmentAsync(departmentReq);
            return CreatedAtAction("GetDepartmentById", new { id = insertedDepartment.DerpartmentID }, insertedDepartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentRequest departmentReq)
        {
            await _departmentService.UpdateDepartmentAsync(id, departmentReq);
            return Ok(departmentReq);
        }
    }

}
