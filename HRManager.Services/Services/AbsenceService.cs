using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
using HRManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Services.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AbsenceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesAsync()
        {
            var absences = await _unitOfWork.AbsenceRepository.GetAbsencesAsync();
            return _mapper.Map<IEnumerable<AbsencesEmployeeResponse>>(absences);
        }

        public async Task<AbsencesEmployeeResponse> GetAbsenceByIdAsync(int absenceId)
        {
            var absence = await _unitOfWork.AbsenceRepository.GetAbsenceByIdAsync(absenceId);
            return _mapper.Map<AbsencesEmployeeResponse>(absence);
        }

        public async Task InsertAbsenceAsync(Absence absence)
        {
            await _unitOfWork.AbsenceRepository.InsertAbsenceAsync(absence);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            await _unitOfWork.AbsenceRepository.DeleteAbsenceAsync(absenceId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAbsenceAsync(Absence absence)
        {
            await _unitOfWork.AbsenceRepository.UpdateAbsenceAsync(absence);
            await _unitOfWork.SaveAsync();
        }
    }

}
