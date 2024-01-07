using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Data.Repositories.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly HRManagerDbContext context;

        public ReportRepository(HRManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Report>> GetReportsAsync()
        {
            return await context.Reports.ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(int reportId)
        {
            return await context.Reports.FindAsync(reportId);
        }

        public async Task InsertReportAsync(Report report)
        {
            await context.Reports.AddAsync(report);
        }

        public async Task DeleteReportAsync(int reportId)
        {
            Report report = await context.Reports.FindAsync(reportId);
            if (report != null)
            {
                context.Reports.Remove(report);
            }
        }

        public async Task UpdateReportAsync(Report report)
        {
            context.Reports.Update(report);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
