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

        [HttpPost("upload-photo")]
        public async Task<ActionResult<PhotoResponse>> UploadPhoto(int employeeId, IFormFile photo)
        {

            var user = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (user == null)
            {
                return NotFound();
            }


            if (photo == null)
            {
                return BadRequest("No file provided.");
            }


            var allowedFileTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
            if (!allowedFileTypes.Contains(photo.ContentType))
            {
                return BadRequest("Invalid file type. Only JPEG, JPG, and PNG are allowed.");
            }


            if (user.Photo != null)
            {
                await DeletePhoto();
            }

            var result = await _photoService.UploadPhotoAsync(photo);

            var photoEntity = new PhotoResponse
            {
                Filename = photo.FileName,
                Uri = result.Uri.ToString()
            };

            user.Photo = photoEntity;
            if (await _employeeService) return _mapper.Map<PhotoDto>(photoEntity);
            return BadRequest("Problem adding photo");
        }

        [HttpDelete("delete-photo")]
        public async Task<IActionResult> DeletePhoto()
        {

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());


            if (user == null)
            {
                return NotFound();
            }

            if (user.Photo != null)
            {
                await _photoService.DeletePhotoAsync(user.Photo.Id, user.Photo.Uri, user.Photo.Filename);
            }


            if (await _userRepository.SaveAllAsync()) return Ok("Deleted");
            return BadRequest("Problem deleting photo");
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
