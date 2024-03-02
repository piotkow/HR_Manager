using System.Collections.Generic;
using System.Threading.Tasks;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace YourApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;

        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsences()
        {
            var absences = await _absenceService.GetAbsencesAsync();
            return absences;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AbsencesEmployeeResponse>> GetAbsenceById(int id)
        {
            var absence = await _absenceService.GetAbsenceByIdAsync(id);

            if (absence == null)
            {
                return NotFound();
            }

            return absence;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAbsence([FromBody] AbsenceRequest absenceReq)
        {
            var insertedAbsence = await _absenceService.InsertAbsenceAsync(absenceReq);
            return CreatedAtAction("GetAbsenceById", new { id = insertedAbsence.AbsenceID }, insertedAbsence);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbsence(int id)
        {
            await _absenceService.DeleteAbsenceAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbsence(int id, [FromBody]AbsenceRequest absenceReq)
        {
            await _absenceService.UpdateAbsenceAsync(id, absenceReq);
            return Ok(absenceReq);
        }
    }

}
