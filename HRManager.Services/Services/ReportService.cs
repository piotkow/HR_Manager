using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.ReportDTO;
using HRManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportEmployeeResponse>> GetReportsAsync()
        {
            var reports = await _unitOfWork.ReportRepository.GetReportsAsync();
            return _mapper.Map<IEnumerable<ReportEmployeeResponse>>(reports);
        }

        public async Task<ReportEmployeeResponse> GetReportByIdAsync(int reportId)
        {
            var report = await _unitOfWork.ReportRepository.GetReportByIdAsync(reportId);
            return _mapper.Map<ReportEmployeeResponse>(report);
        }

        public async Task InsertReportAsync(Report report)
        {
            await _unitOfWork.ReportRepository.InsertReportAsync(report);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteReportAsync(int reportId)
        {
            await _unitOfWork.ReportRepository.DeleteReportAsync(reportId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateReportAsync(Report report)
        {
            await _unitOfWork.ReportRepository.UpdateReportAsync(report);
            await _unitOfWork.SaveAsync();
        }
    }


}
