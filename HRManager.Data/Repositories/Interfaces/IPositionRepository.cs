using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IPositionRepository : IDisposable
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<Position> GetPositionByIdAsync(int positionId);
        Task InsertPositionAsync(Position position);
        Task DeletePositionAsync(int positionId);
        Task UpdatePositionAsync(Position position);
        Task SaveAsync();
    }

}
