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
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            return await _reportRepository.GetReportsAsync();
        }

        public async Task<Report> GetReportByIdAsync(int reportId)
        {
            return await _reportRepository.GetReportByIdAsync(reportId);
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
