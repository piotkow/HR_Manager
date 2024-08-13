using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.TeamDTO;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDepartmentResponse>> GetTeamsAsync()
        {
            var teams = await _unitOfWork.TeamRepository.GetTeamsAsync();
            return _mapper.Map<IEnumerable<TeamDepartmentResponse>>(teams);
        }

        public async Task<TeamDepartmentResponse> GetTeamByIdAsync(int teamId)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamByIdAsync(teamId);
            return _mapper.Map<TeamDepartmentResponse>(team);
        }

        public async Task<Team> InsertTeamAsync(TeamRequest teamReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var team = _mapper.Map<Team>(teamReq);
            await _unitOfWork.TeamRepository.InsertTeamAsync(team);
            await _unitOfWork.CommitAsync();
            return team;
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            await _unitOfWork.TeamRepository.DeleteTeamAsync(teamId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTeamAsync(int teamId, TeamRequest teamReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var team = await _unitOfWork.TeamRepository.GetTeamByIdAsync(teamId);
            _mapper.Map(teamReq, team);
            await _unitOfWork.TeamRepository.UpdateTeamAsync(team);
            await _unitOfWork.CommitAsync();
        }
    }

}
