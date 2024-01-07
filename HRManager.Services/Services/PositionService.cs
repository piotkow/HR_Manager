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
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            return await _positionRepository.GetPositionsAsync();
        }

        public async Task<Position> GetPositionByIdAsync(int positionId)
        {
            return await _positionRepository.GetPositionByIdAsync(positionId);
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
