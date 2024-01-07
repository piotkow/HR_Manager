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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeePositionTeamResponse>> GetPositionsAsync()
        {
            var positions = await _positionRepository.GetPositionsAsync();
            return _mapper.Map<IEnumerable<EmployeePositionTeamResponse>>(positions);
        }

        public async Task<EmployeePositionTeamResponse> GetPositionByIdAsync(int positionId)
        {
            var position = await _positionRepository.GetPositionByIdAsync(positionId);
            return _mapper.Map<EmployeePositionTeamResponse>(position);
        }


        public async Task InsertPositionAsync(Position position)
        {
            await _positionRepository.InsertPositionAsync(position);
        }

        public async Task DeletePositionAsync(int positionId)
        {
            await _positionRepository.DeletePositionAsync(positionId);
        }

        public async Task UpdatePositionAsync(Position position)
        {
            await _positionRepository.UpdatePositionAsync(position);
        }
    }

}
