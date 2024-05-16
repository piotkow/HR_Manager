using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.Repositories.Repositories;
using Microsoft.EntityFrameworkCore.Storage;


namespace HRManager.Data.UnitOfWork
{
    public class UnitOfWork(HRManagerDbContext context) : IUnitOfWork
    {
        private readonly HRManagerDbContext context = context;
        private IDbContextTransaction _transaction;

        public IAccountRepository AccountRepository { get; private set; } = new AccountRepository(context);
        public IDocumentRepository DocumentRepository { get; private set; } = new DocumentRepository(context);
        public IEmployeeRepository EmployeeRepository { get; private set; } = new EmployeeRepository(context);
        public IPositionRepository PositionRepository { get; private set; } = new PositionRepository(context);
        public IReportRepository ReportRepository { get; private set; } = new ReportRepository(context);
        public ITeamRepository TeamRepository { get; private set; } = new TeamRepository(context);
        public IAbsenceRepository AbsenceRepository { get; private set; } = new AbsenceRepository(context);
        public IPhotoRepository PhotoRepository { get; private set; } = new PhotoRepository(context);
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }

}
