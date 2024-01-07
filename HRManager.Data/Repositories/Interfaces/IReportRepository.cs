using HRManager.Models.Entities;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IReportRepository : IDisposable
    {
        Task<IEnumerable<Report>> GetReportsAsync();
        Task<Report> GetReportByIdAsync(int reportId);
        Task InsertReportAsync(Report report);
        Task DeleteReportAsync(int reportId);
        Task UpdateReportAsync(Report report);
        Task SaveAsync();
    }

}
