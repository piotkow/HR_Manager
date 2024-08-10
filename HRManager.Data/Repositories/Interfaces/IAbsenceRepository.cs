using HRManager.Models.Entities;
using HRManager.Models.Enums;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IAbsenceRepository : IDisposable
    {
        Task<IEnumerable<Absence>> GetAbsencesAsync();
        Task<Absence> GetAbsenceByIdAsync(int absenceId);
        Task<IEnumerable<Absence>> GetAbsencesByEmployeeAsync(int employeeId);
        Task<IEnumerable<Absence>> GetAbsencesByTeamAsync(int teamId);
        Task InsertAbsenceAsync(Absence absence);
        Task DeleteAbsenceAsync(int absenceId);
        Task UpdateAbsenceAsync(Absence absence);
        Task<IEnumerable<Absence>> GetAbsencesByStatus(Status status);
        Task SaveAsync();
    }

}
