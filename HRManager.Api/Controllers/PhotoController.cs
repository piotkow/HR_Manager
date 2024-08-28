using HRManager.Models.Entities;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<IEnumerable<FileResponse>> GetPhotos()
        {
            var photos = await _photoService.GetPhotosAsync();
            return photos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileResponse>> GetPhotoById(int id)
        {
            var photo = await _photoService.GetPhotoByIdAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        [HttpPost("upload-photo/{employeeId}")]
        public async Task<ActionResult> UploadPhoto(IFormFile photo, int employeeId)
        {
            if (photo == null)
            {
                return BadRequest("No file provided.");
            }

            var allowedFileTypes = new[]
            {
                "image/jpeg", "image/jpg", "image/png"
            };

            if (!allowedFileTypes.Contains(photo.ContentType))
            {
                return BadRequest("Invalid file type. Only JPG and PNG images are allowed.");
            }

            var result = await _photoService.UploadPhotoAsync(photo, employeeId);
            if (result != null) return Ok(result);
            return BadRequest("Problem uploading file");
        }

        [HttpPost]
        public async Task<IActionResult> InsertPhoto([FromBody] PhotoRequest photoReq)
        {
            var insertedPhoto = await _photoService.InsertPhotoAsync(photoReq);
            return CreatedAtAction("GetPhotoById", new { id = insertedPhoto.PhotoID }, insertedPhoto);
        }

        [HttpDelete("{id}/{employeeId}")]
        public async Task<IActionResult> DeletePhoto(int id, int employeeId)
        {
            var photoToDelete = await _photoService.GetPhotoByIdAsync(id);

            if (photoToDelete == null)
            {
                return NotFound();
            }

            await _photoService.DeletePhotoAsync(id, photoToDelete.Uri, photoToDelete.Filename, employeeId);
            return Ok();
        }

        [HttpPut("{id}/{employeeId}")]
        public async Task<IActionResult> UpdatePhoto(int id, IFormFile photo, int employeeId)
        {
            await _photoService.UpdatePhotoAsync(id, photo, employeeId);
            return Ok();
        }
    }
}
