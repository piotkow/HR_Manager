using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await _teamRepository.GetTeamsAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await _teamRepository.GetTeamByIdAsync(teamId);
        }

        public async Task InsertTeamAsync(Team team)
        {
            await _teamRepository.InsertTeamAsync(team);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            await _teamRepository.DeleteTeamAsync(teamId);
        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _teamRepository.UpdateTeamAsync(team);
        }
    }

}
