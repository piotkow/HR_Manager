using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
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
        private readonly IMapper _mapper;

        public AbsenceService(IAbsenceRepository absenceRepository, IMapper mapper)
        {
            _absenceRepository = absenceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesAsync()
        {
            var absences = await _absenceRepository.GetAbsencesAsync();
            return _mapper.Map<IEnumerable<AbsencesEmployeeResponse>>(absences);
        }

        public async Task<AbsencesEmployeeResponse> GetAbsenceByIdAsync(int absenceId)
        {
            var absence = await _absenceRepository.GetAbsenceByIdAsync(absenceId);
            return _mapper.Map<AbsencesEmployeeResponse>(absence);
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
