using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.TeamDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task<Team> InsertTeamAsync(TeamRequest teamReq);
        Task DeleteTeamAsync(int teamId);
        Task UpdateTeamAsync(int teamId, TeamRequest teamReq);
    }
}
