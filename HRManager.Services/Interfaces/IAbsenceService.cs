using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IAbsenceService
    {
        Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesAsync();
        Task<AbsencesEmployeeResponse> GetAbsenceByIdAsync(int absenceId);
        Task InsertAbsenceAsync(Absence absence);
        Task DeleteAbsenceAsync(int absenceId);
        Task UpdateAbsenceAsync(Absence absence);
    }



}
