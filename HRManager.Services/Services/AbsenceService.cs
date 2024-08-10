using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Models.Enums;
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

        public async Task<Absence> InsertAbsenceAsync(AbsenceRequest absenceReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var absence = _mapper.Map<Absence>(absenceReq);
            await _unitOfWork.AbsenceRepository.InsertAbsenceAsync(absence);
            await _unitOfWork.CommitAsync();
            return absence;
        }

        public async Task DeleteAbsenceAsync(int absenceId)
        {
            await _unitOfWork.AbsenceRepository.DeleteAbsenceAsync(absenceId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAbsenceAsync(int absenceId, AbsenceRequest absenceReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var absence = await _unitOfWork.AbsenceRepository.GetAbsenceByIdAsync(absenceId);
            absence.EmployeeID= absenceReq.EmployeeID;
            absence.StartDate= absenceReq.StartDate;
            absence.EndDate= absenceReq.EndDate;
            absence.Status = absenceReq.Status;
            absence.RejectionReason= absenceReq.RejectionReason;
            await _unitOfWork.AbsenceRepository.UpdateAbsenceAsync(absence);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByStatusAsync(Status status)
        {
            var absences = await _unitOfWork.AbsenceRepository.GetAbsencesByStatus(status);
            return _mapper.Map<IEnumerable<AbsencesEmployeeResponse>>(absences);
        }

        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByEmployeeAsync(int employeeId)
        {
            var absences = await _unitOfWork.AbsenceRepository.GetAbsencesByEmployeeAsync(employeeId);
            return _mapper.Map<IEnumerable<AbsencesEmployeeResponse>>(absences);
        }

        public async Task<IEnumerable<AbsencesEmployeeResponse>> GetAbsencesByTeamAsync(int teamId)
        {
            var absences = await _unitOfWork.AbsenceRepository.GetAbsencesByTeamAsync(teamId);
            return _mapper.Map<IEnumerable<AbsencesEmployeeResponse>>(absences);
        }

        public async Task<Absence> UpdateAbsenceStatusAsync(int absenceId, Status status)
        {
            await _unitOfWork.BeginTransactionAsync();
            var absence = await _unitOfWork.AbsenceRepository.GetAbsenceByIdAsync(absenceId);
            absence.Status = status;
            await _unitOfWork.CommitAsync();
            return absence;
        }
    }

}
