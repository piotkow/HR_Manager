using HRManager.Models.Entities;
using HRManager.Services.DTOs.EmployeeDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<EmployeePositionTeamResponse>> GetPositionsAsync();
        Task<EmployeePositionTeamResponse> GetPositionByIdAsync(int positionId);
        Task InsertPositionAsync(Position position);
        Task DeletePositionAsync(int positionId);
        Task UpdatePositionAsync(Position position);
    }



}
