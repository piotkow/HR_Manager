using AutoMapper;
using HRManager.Data.Repositories.Interfaces;
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
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportEmployeeResponse>> GetReportsAsync()
        {
            var reports = await _reportRepository.GetReportsAsync();
            return _mapper.Map<IEnumerable<ReportEmployeeResponse>>(reports);
        }

        public async Task<ReportEmployeeResponse> GetReportByIdAsync(int reportId)
        {
            var report = await _reportRepository.GetReportByIdAsync(reportId);
            return _mapper.Map<ReportEmployeeResponse>(report);
        }

        public async Task InsertReportAsync(Report report)
        {
            await _reportRepository.InsertReportAsync(report);
        }

        public async Task DeleteReportAsync(int reportId)
        {
            await _reportRepository.DeleteReportAsync(reportId);
        }

        public async Task UpdateReportAsync(Report report)
        {
            await _reportRepository.UpdateReportAsync(report);
        }
    }

}
