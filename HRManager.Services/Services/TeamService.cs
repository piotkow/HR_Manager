using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public TeamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            var teams = await _unitOfWork.TeamRepository.GetTeamsAsync();
            return teams;
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            var team = await _unitOfWork.TeamRepository.GetTeamByIdAsync(teamId);
            return team;
        }

        public async Task InsertTeamAsync(Team team)
        {
            await _unitOfWork.TeamRepository.InsertTeamAsync(team);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            await _unitOfWork.TeamRepository.DeleteTeamAsync(teamId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTeamAsync(Team team)
        {
            await _unitOfWork.TeamRepository.UpdateTeamAsync(team);
            await _unitOfWork.SaveAsync();
        }
    }

}
