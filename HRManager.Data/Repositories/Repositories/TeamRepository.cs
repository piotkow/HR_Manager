using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly HRManagerDbContext context;

        public TeamRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamByIdAsync(int teamId)
        {
            return await context.Teams.FindAsync(teamId);
        }

        public async Task InsertTeamAsync(Team team)
        {
            await context.Teams.AddAsync(team);
        }

        public async Task DeleteTeamAsync(int teamId)
        {
            Team team = await context.Teams.FindAsync(teamId);
            if (team != null)
            {
                context.Teams.Remove(team);
            }
        }

        public async Task UpdateTeamAsync(Team team)
        {
            context.Teams.Update(team);
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
