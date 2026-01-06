using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using HRManager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 5: Operacje masowe (Bulk Operations)
/// 
/// Porównanie:
/// - Standardowego dodawania/edycji rekordów w pêtli
/// - Metody AddRange
/// - Rozszerzeñ do operacji Bulk (EFCore.BulkExtensions)
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class BulkOperationsBenchmarks : BenchmarkBase
{
    [Params(10, 100, 500)]
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
        // Przygotuj dane testowe przed ka¿d¹ iteracj¹
        _testAbsences = GenerateTestAbsences(RecordCount);
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        // Wyczyœæ dane testowe po ka¿dej iteracji
        using var context = CreateContext();
        var testAbsences = context.Absences
            .Where(a => a.Description != null && a.Description.StartsWith("BENCHMARK_TEST_"))
            .ToList();
        
        if (testAbsences.Any())
        {
            context.Absences.RemoveRange(testAbsences);
            context.SaveChanges();
        }
    }

    private List<Absence> GenerateTestAbsences(int count)
    {
        var absences = new List<Absence>();
        var random = new Random(42); // Sta³y seed dla powtarzalnoœci

        for (int i = 0; i < count; i++)
        {
            absences.Add(new Absence
            {
                EmployeeID = _testEmployeeId,
                Description = $"BENCHMARK_TEST_{Guid.NewGuid()}",
                StartDate = DateTime.Now.AddDays(random.Next(1, 30)),
                EndDate = DateTime.Now.AddDays(random.Next(31, 60)),
                Status = Status.Pending
            });
        }

        return absences;
    }

    #region INSERT Operations

    /// <summary>
    /// Standardowe dodawanie w pêtli z SaveChanges po ka¿dym rekordzie
    /// Najwolniejsze - N operacji SaveChanges
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task Insert_Loop_SaveEach()
    {
        using var context = CreateContext();
        
        foreach (var absence in _testAbsences)
        {
            context.Absences.Add(absence);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Dodawanie w pêtli z jednym SaveChanges na koñcu
    /// </summary>
    [Benchmark]
    public async Task Insert_Loop_SaveOnce()
    {
        using var context = CreateContext();
        
        foreach (var absence in _testAbsences)
        {
            context.Absences.Add(absence);
        }
        
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// AddRange z jednym SaveChanges
    /// Zalecane podejœcie standardowe EF Core
    /// </summary>
    [Benchmark]
    public async Task Insert_AddRange()
    {
        using var context = CreateContext();
        
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// BulkInsert z EFCore.BulkExtensions
    /// Najszybsze dla du¿ych zbiorów danych
    /// </summary>
    [Benchmark]
    public async Task Insert_BulkExtensions()
    {
        using var context = CreateContext();
        
        await context.BulkInsertAsync(_testAbsences);
    }

    #endregion

    #region UPDATE Operations

    /// <summary>
    /// Standardowe aktualizacje - pobierz, zmieñ, zapisz ka¿dy osobno
    /// </summary>
    [Benchmark]
    public async Task Update_Loop_SaveEach()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();

        // Aktualizuj ka¿dy rekord osobno
        foreach (var absence in _testAbsences)
        {
            absence.Description = "BENCHMARK_TEST_UPDATED_" + Guid.NewGuid();
            context.Absences.Update(absence);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// UpdateRange z jednym SaveChanges
    /// </summary>
    [Benchmark]
    public async Task Update_UpdateRange()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();

        // Aktualizuj wszystkie naraz
        foreach (var absence in _testAbsences)
        {
            absence.Description = "BENCHMARK_TEST_UPDATED_" + Guid.NewGuid();
        }
        
        context.Absences.UpdateRange(_testAbsences);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// ExecuteUpdate (EF Core 7.0+) - aktualizacja bez pobierania encji
    /// </summary>
    [Benchmark]
    public async Task Update_ExecuteUpdate()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();

        // Aktualizuj bezpoœrednio w bazie bez pobierania
        await context.Absences
            .Where(a => a.Description != null && a.Description.StartsWith("BENCHMARK_TEST_"))
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Description, a => "BENCHMARK_TEST_BULK_UPDATED"));
    }

    /// <summary>
    /// BulkUpdate z EFCore.BulkExtensions
    /// </summary>
    [Benchmark]
    public async Task Update_BulkExtensions()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        await context.BulkInsertAsync(_testAbsences);

        // Aktualizuj wszystkie
        foreach (var absence in _testAbsences)
        {
            absence.Description = "BENCHMARK_TEST_UPDATED_" + Guid.NewGuid();
        }
        
        await context.BulkUpdateAsync(_testAbsences);
    }

    #endregion

    #region DELETE Operations

    /// <summary>
    /// ExecuteDelete (EF Core 7.0+) - usuwanie bez pobierania encji
    /// </summary>
    [Benchmark]
    public async Task Delete_ExecuteDelete()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        context.Absences.AddRange(_testAbsences);
        await context.SaveChangesAsync();

        // Usuñ bezpoœrednio w bazie
        await context.Absences
            .Where(a => a.Description != null && a.Description.StartsWith("BENCHMARK_TEST_"))
            .ExecuteDeleteAsync();
    }

    /// <summary>
    /// BulkDelete z EFCore.BulkExtensions
    /// </summary>
    [Benchmark]
    public async Task Delete_BulkExtensions()
    {
        using var context = CreateContext();
        
        // Najpierw wstaw dane
        await context.BulkInsertAsync(_testAbsences);

        // Usuñ wszystkie
        await context.BulkDeleteAsync(_testAbsences);
    }

    #endregion
}
