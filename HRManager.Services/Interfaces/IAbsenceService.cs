using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IAbsenceService
    {
        Task<IEnumerable<Absence>> GetAbsencesAsync();
        Task<Absence> GetAbsenceByIdAsync(int absenceId);
        Task InsertAbsenceAsync(Absence absence);
        Task DeleteAbsenceAsync(int absenceId);
        Task UpdateAbsenceAsync(Absence absence);
    }

}
