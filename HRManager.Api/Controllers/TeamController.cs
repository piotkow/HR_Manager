using HRManager.Models.Entities;
using HRManager.Services.DTOs.TeamDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamDepartmentResponse>> GetTeams()
        {
            var teams = await _teamService.GetTeamsAsync();
            return teams;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDepartmentResponse>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }


        [HttpPost]
        public async Task<IActionResult> InsertTeam([FromBody] TeamRequest teamReq)
        {
            var insertedTeam = await _teamService.InsertTeamAsync(teamReq);
            return CreatedAtAction("GetTeamById", new { id = insertedTeam.TeamID }, insertedTeam);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamRequest teamReq)
        {
            await _teamService.UpdateTeamAsync(id, teamReq);
            return Ok(teamReq);
        }
    }

}
