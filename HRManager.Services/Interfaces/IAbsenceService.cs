using HRManager.Models.Entities;
using HRManager.Models.Enums;
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
        Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByEmployeeAsync(int employeeId);
        Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByTeamAsync(int teamId);
        Task<Absence> InsertAbsenceAsync(AbsenceRequest absenceReq);
        Task DeleteAbsenceAsync(int absenceId);
        Task UpdateAbsenceAsync(int absenceId, AbsenceRequest absenceReq);
        Task<Absence>UpdateAbsenceStatusAsync(int absenceId, Status status);
        Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByStatusAsync(Status status);
    }



}
