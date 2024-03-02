using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.PositionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<Position> GetPositionByIdAsync(int positionId);
        Task<Position> InsertPositionAsync(PositionRequest positionReq);
        Task DeletePositionAsync(int positionId);
        Task UpdatePositionAsync(int positionId, PositionRequest positionReq);
    }



}
