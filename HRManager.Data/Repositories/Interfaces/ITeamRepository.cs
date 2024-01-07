using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface ITeamRepository : IDisposable
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamByIdAsync(int teamId);
        Task InsertTeamAsync(Team team);
        Task DeleteTeamAsync(int teamId);
        Task UpdateTeamAsync(Team team);
        Task SaveAsync();
    }

}
