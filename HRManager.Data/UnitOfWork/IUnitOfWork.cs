﻿using HRManager.Data.Repositories.Interfaces;

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
        Task SaveAsync();
    }

}
