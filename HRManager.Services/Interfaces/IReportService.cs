using HRManager.Models.Entities;
using HRManager.Services.DTOs.ReportDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<ReportEmployeeResponse>> GetReportsAsync();
        Task<ReportEmployeeResponse> GetReportByIdAsync(int reportId);
        Task InsertReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
        Task UpdateReportAsync(Report report);
    }



}
