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
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            var positions = await _unitOfWork.PositionRepository.GetPositionsAsync();
            return positions;
        }

        public async Task<Position> GetPositionByIdAsync(int positionId)
        {
            var position = await _unitOfWork.PositionRepository.GetPositionByIdAsync(positionId);
            return position;
        }

        public async Task InsertPositionAsync(Position position)
        {
            await _unitOfWork.PositionRepository.InsertPositionAsync(position);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeletePositionAsync(int positionId)
        {
            await _unitOfWork.PositionRepository.DeletePositionAsync(positionId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePositionAsync(Position position)
        {
            await _unitOfWork.PositionRepository.UpdatePositionAsync(position);
            await _unitOfWork.SaveAsync();
        }
    }

}
