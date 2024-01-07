using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IAbsenceRepository _absenceRepository;

        public AbsenceService(IAbsenceRepository absenceRepository)
        {
            _absenceRepository = absenceRepository;
        }

        public async Task<IEnumerable<Absence>> GetAbsencesAsync()
        {
            return await _absenceRepository.GetAbsencesAsync();
        }

        public async Task<Absence> GetAbsenceByIdAsync(int absenceId)
        {
            return await _absenceRepository.GetAbsenceByIdAsync(absenceId);
        }

        public async Task InsertAbsenceAsync(Absence absence)
        {
            await _absenceRepository.InsertAbsenceAsync(absence);
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            await _absenceRepository.DeleteAbsenceAsync(absenceId);
        }

        public async Task UpdateAbsenceAsync(Absence absence)
        {
            await _absenceRepository.UpdateAbsenceAsync(absence);
        }
    }

}
