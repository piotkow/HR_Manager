using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Models.Enums;
using Microsoft.EntityFrameworkCore;


namespace HRManager.Data.Repositories.Repositories
{
    public class AbsenceRepository : IAbsenceRepository
    {
        private readonly HRManagerDbContext context;

        public AbsenceRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Absence>> GetAbsencesAsync()
        {
            return await context.Absences.Include(e=>e.Employee).ToListAsync();
        }

        public async Task<Absence> GetAbsenceByIdAsync(int absenceId)
        {
            return await context.Absences.FirstOrDefaultAsync(a => a.AbsenceID==absenceId);
        }

        public async Task InsertAbsenceAsync(Absence absence)
        {
            await context.Absences.AddAsync(absence);
        }

        public async Task<IEnumerable<Absence>> GetAbsencesByStatus(Status status)
        {
            return await context.Absences.Include(e=>e.Employee).Where(a=>a.Status==status).ToListAsync();
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            Absence absence = await context.Absences.FindAsync(absenceId);
            if (absence != null)
            {
                context.Absences.Remove(absence);
            }
        }

        public async Task UpdateAbsenceAsync(Absence absence)
        {
            context.Absences.Update(absence);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<IEnumerable<Absence>> GetAbsencesByEmployeeAsync(int employeeId)
        {
            return await context.Absences.Where(a => a.EmployeeID == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Absence>> GetAbsencesByTeamAsync(int teamId)
        {
            return await context.Absences.Include(a=>a.Employee).ThenInclude(e => e.Team).Where(a => a.Employee.Team.TeamID == teamId).ToListAsync();
        }

    }


}
