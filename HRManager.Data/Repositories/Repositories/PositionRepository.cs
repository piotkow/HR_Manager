using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly HRManagerDbContext context;

        public PositionRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            return await context.Positions.ToListAsync();
        }

        public async Task<Position> GetPositionByIdAsync(int positionId)
        {
            return await context.Positions.FindAsync(positionId);
        }

        public async Task InsertPositionAsync(Position position)
        {
            await context.Positions.AddAsync(position);
        }

        public async Task DeletePositionAsync(int positionId)
        {
            Position position = await context.Positions.FindAsync(positionId);
            if (position != null)
            {
                context.Positions.Remove(position);
            }
        }

        public async Task UpdatePositionAsync(Position position)
        {
            context.Positions.Update(position);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
