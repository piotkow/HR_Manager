using HRManager.Models.Entities;
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
        Task InsertTeamAsync(Team team);
        Task DeleteTeamAsync(int teamId);
        Task UpdateTeamAsync(Team team);
    }

}
