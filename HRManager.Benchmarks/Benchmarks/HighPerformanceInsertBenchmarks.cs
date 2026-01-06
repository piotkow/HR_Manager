using BenchmarkDotNet.Attributes;
using Dapper;
using EFCore.BulkExtensions;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using HRManager.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 8: Szybki zapis (High Performance Insert)
/// 
/// Masowe wstawianie danych do bazy (np. import danych).
/// Porównanie ró¿nych podejœæ: EF Core, Dapper, ADO.NET, Bulk Extensions, SqlBulkCopy.
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class HighPerformanceInsertBenchmarks : BenchmarkBase
{
    [Params(100, 500, 1000)]
    public int RecordCount { get; set; }

    private List<Absence> _testAbsences = new();
    private int _testEmployeeId = 1;

    [GlobalSetup]
    public void Setup()
    {
        using var context = CreateContext();
        var employee = context.Employees.FirstOrDefault();
        if (employee != null)
        {
            _testEmployeeId = employee.EmployeeID;
        }
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _testAbsences = GenerateTestAbsences(RecordCount);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        using var context = CreateContext();
        context.Database.ExecuteSqlRaw(
            "DELETE FROM Absences WHERE Description LIKE 'PERF_TEST_%'");
    }

    private List<Absence> GenerateTestAbsences(int count)
    {
        var absences = new List<Absence>();
        var random = new Random(42);

        for (int i = 0; i < count; i++)
        {
            absences.Add(new Absence
            {
                EmployeeID = _testEmployeeId,
                Description = $"PERF_TEST_{Guid.NewGuid()}",
                StartDate = DateTime.Now.AddDays(random.Next(1, 30)),
                EndDate = DateTime.Now.AddDays(random.Next(31, 60)),
                Status = Status.Pending
            });
        }

        return absences;
    }

    #region EF Core

    /// <summary>
    /// EF Core - AddRange (standardowe podejœcie)
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task EFCore_AddRange()
    {
        using var context = CreateContext();
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// EF Core - AddRange z wy³¹czonym AutoDetectChanges
    /// </summary>
    [Benchmark]
    public async Task EFCore_AddRange_NoAutoDetect()
    {
        using var context = CreateContext();
        context.ChangeTracker.AutoDetectChangesEnabled = false;
        
        try
        {
            context.Absences.AddRange(_testAbsences);
            await context.SaveChangesAsync();
        }
        finally
        {
            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }
    }

    /// <summary>
    /// EF Core - BulkInsert (EFCore.BulkExtensions)
    /// Najszybsza metoda dla EF Core
    /// </summary>
    [Benchmark]
    public async Task EFCore_BulkInsert()
    {
        using var context = CreateContext();
        await context.BulkInsertAsync(_testAbsences);
    }

    /// <summary>
    /// EF Core - BulkInsert z konfiguracj¹
    /// </summary>
    [Benchmark]
    public async Task EFCore_BulkInsert_Configured()
    {
        using var context = CreateContext();
        await context.BulkInsertAsync(_testAbsences, options =>
        {
            options.BatchSize = 500;
            options.SetOutputIdentity = false; // Szybciej gdy nie potrzebujemy ID
        });
    }

    #endregion

    #region Dapper

    /// <summary>
    /// Dapper - Execute w pêtli (pojedyncze INSERT)
    /// </summary>
    [Benchmark]
    public async Task Dapper_Execute_Loop()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var sql = @"INSERT INTO Absences (EmployeeID, Description, StartDate, EndDate, Status, RejectionReason) 
                    VALUES (@EmployeeID, @Description, @StartDate, @EndDate, @Status, @RejectionReason)";

        foreach (var absence in _testAbsences)
        {
            await connection.ExecuteAsync(sql, new
            {
                absence.EmployeeID,
                absence.Description,
                absence.StartDate,
                absence.EndDate,
                Status = (int)absence.Status,
                absence.RejectionReason
            });
        }
    }

    /// <summary>
    /// Dapper - Execute z kolekcj¹ (batch insert)
    /// Dapper automatycznie wykonuje w batch
    /// </summary>
    [Benchmark]
    public async Task Dapper_Execute_Batch()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var sql = @"INSERT INTO Absences (EmployeeID, Description, StartDate, EndDate, Status, RejectionReason) 
                    VALUES (@EmployeeID, @Description, @StartDate, @EndDate, @Status, @RejectionReason)";

        await connection.ExecuteAsync(sql, _testAbsences.Select(a => new
        {
            a.EmployeeID,
            a.Description,
            a.StartDate,
            a.EndDate,
            Status = (int)a.Status,
            a.RejectionReason
        }));
    }

    #endregion

    #region ADO.NET

    /// <summary>
    /// ADO.NET - SqlCommand w pêtli
    /// </summary>
    [Benchmark]
    public async Task AdoNet_Command_Loop()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var sql = @"INSERT INTO Absences (EmployeeID, Description, StartDate, EndDate, Status, RejectionReason) 
                    VALUES (@EmployeeID, @Description, @StartDate, @EndDate, @Status, @RejectionReason)";

        foreach (var absence in _testAbsences)
        {
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@EmployeeID", absence.EmployeeID);
            command.Parameters.AddWithValue("@Description", absence.Description ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@StartDate", absence.StartDate);
            command.Parameters.AddWithValue("@EndDate", absence.EndDate);
            command.Parameters.AddWithValue("@Status", (int)absence.Status);
            command.Parameters.AddWithValue("@RejectionReason", absence.RejectionReason ?? (object)DBNull.Value);
            
            await command.ExecuteNonQueryAsync();
        }
    }

    /// <summary>
    /// ADO.NET - SqlBulkCopy (najszybsza metoda ADO.NET)
    /// </summary>
    [Benchmark]
    public async Task AdoNet_SqlBulkCopy()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        using var bulkCopy = new SqlBulkCopy(connection)
        {
            DestinationTableName = "Absences",
            BatchSize = 500
        };

        // Mapowanie kolumn
        bulkCopy.ColumnMappings.Add("EmployeeID", "EmployeeID");
        bulkCopy.ColumnMappings.Add("Description", "Description");
        bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
        bulkCopy.ColumnMappings.Add("EndDate", "EndDate");
        bulkCopy.ColumnMappings.Add("Status", "Status");
        bulkCopy.ColumnMappings.Add("RejectionReason", "RejectionReason");

        var dataTable = CreateDataTable();
        await bulkCopy.WriteToServerAsync(dataTable);
    }

    /// <summary>
    /// ADO.NET - SqlBulkCopy zoptymalizowany
    /// </summary>
    [Benchmark]
    public async Task AdoNet_SqlBulkCopy_Optimized()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        using var bulkCopy = new SqlBulkCopy(connection, Microsoft.Data.SqlClient.SqlBulkCopyOptions.TableLock, null)
        {
            DestinationTableName = "Absences",
            BatchSize = 1000,
            EnableStreaming = true
        };

        bulkCopy.ColumnMappings.Add("EmployeeID", "EmployeeID");
        bulkCopy.ColumnMappings.Add("Description", "Description");
        bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
        bulkCopy.ColumnMappings.Add("EndDate", "EndDate");
        bulkCopy.ColumnMappings.Add("Status", "Status");
        bulkCopy.ColumnMappings.Add("RejectionReason", "RejectionReason");

        var dataTable = CreateDataTable();
        await bulkCopy.WriteToServerAsync(dataTable);
    }

    private DataTable CreateDataTable()
    {
        var table = new DataTable();
        table.Columns.Add("EmployeeID", typeof(int));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("StartDate", typeof(DateTime));
        table.Columns.Add("EndDate", typeof(DateTime));
        table.Columns.Add("Status", typeof(int));
        table.Columns.Add("RejectionReason", typeof(string));

        foreach (var absence in _testAbsences)
        {
            table.Rows.Add(
                absence.EmployeeID,
                absence.Description,
                absence.StartDate,
                absence.EndDate,
                (int)absence.Status,
                absence.RejectionReason ?? (object)DBNull.Value
            );
        }

        return table;
    }

    #endregion

    #region Transakcje

    /// <summary>
    /// EF Core - BulkInsert w transakcji
    /// </summary>
    [Benchmark]
    public async Task EFCore_BulkInsert_WithTransaction()
    {
        using var context = CreateContext();
        using var transaction = await context.Database.BeginTransactionAsync();
        
        try
        {
            await context.BulkInsertAsync(_testAbsences);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    #endregion
}
