using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> GetReportByIdAsync(int reportId);
        Task InsertReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
        Task UpdateReportAsync(Report report);
    }

}
