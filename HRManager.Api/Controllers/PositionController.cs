using HRManager.Models.Entities;
using HRManager.Services.DTOs.PositionDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Position>> GetPositions()
        {
            var positions = await _positionService.GetPositionsAsync();
            return positions;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Position>> GetPositionById(int id)
        {
            var position = await _positionService.GetPositionByIdAsync(id);

            if (position == null)
            {
                return NotFound();
            }

            return position;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPosition([FromBody] PositionRequest positionReq)
        {
            var insertedPosition = await _positionService.InsertPositionAsync(positionReq);
            return CreatedAtAction("GetPositionById", new { id = insertedPosition.PositionID }, insertedPosition);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            await _positionService.DeletePositionAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(int id, [FromBody] PositionRequest positionReq)
        {
            await _positionService.UpdatePositionAsync(id, positionReq);
            return Ok(positionReq);
        }
    }

}
