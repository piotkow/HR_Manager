using HRManager.Data.Repositories.Interfaces;

namespace HRManager.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IPositionRepository PositionRepository { get; }
        IReportRepository ReportRepository { get; }
        ITeamRepository TeamRepository { get; }
        IAbsenceRepository AbsenceRepository { get; }
        IPhotoRepository PhotoRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        Task SaveAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }

}
