using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
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
        private readonly IMapper _mapper;


        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetTeamsAsync()
        {
            var teams = await _teamRepository.GetTeamsAsync();
            return _mapper.Map<IEnumerable<EmployeePositionTeamResponse>>(teams);
        }

        public async Task<EmployeePositionTeamResponse> GetTeamByIdAsync(int teamId)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            return _mapper.Map<EmployeePositionTeamResponse>(team);
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
