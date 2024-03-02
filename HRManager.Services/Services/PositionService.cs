using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.PositionDTO;
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
        private readonly IMapper _mapper;

        public PositionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<Position> InsertPositionAsync(PositionRequest positionReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var position = _mapper.Map<Position>(positionReq);
            await _unitOfWork.PositionRepository.InsertPositionAsync(position);
            await _unitOfWork.CommitAsync();
            return position;
        }

        public async Task DeletePositionAsync(int positionId)
        {
            await _unitOfWork.PositionRepository.DeletePositionAsync(positionId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePositionAsync(int positionId, PositionRequest positionReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var position = await _unitOfWork.PositionRepository.GetPositionByIdAsync(positionId);
            _mapper.Map(positionReq, position);
            await _unitOfWork.PositionRepository.UpdatePositionAsync(position);
            await _unitOfWork.CommitAsync();
        }
    }

}
