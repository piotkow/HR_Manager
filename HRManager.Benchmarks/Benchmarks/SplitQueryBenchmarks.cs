using BenchmarkDotNet.Attributes;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 4: Problem iloczynu kartezjañskiego (Cartesian Explosion) i Split Queries
/// 
/// Porównanie wydajnoœci pojedynczego du¿ego zapytania z wieloma z³¹czeniami (JOIN)
/// vs mechanizm Query Splitting (rozbicie na osobne zapytania SQL) dostêpny w EF Core.
/// 
/// Problem Cartesian Explosion wystêpuje gdy mamy wiele kolekcji Include - 
/// iloœæ zwracanych wierszy = iloczyn kardynalnoœci wszystkich kolekcji.
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci, Liczba zapytañ SQL
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class SplitQueryBenchmarks : BenchmarkBase
{
    [Params(5, 20, 50)]
    public int RecordCount { get; set; }

    #region Single Query vs Split Query - podstawowe

    /// <summary>
    /// Single Query (domyœlne) - jedno du¿e zapytanie z wieloma JOIN
    /// Mo¿e powodowaæ Cartesian Explosion przy wielu kolekcjach
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<List<Employee>> SingleQuery_MultipleIncludes()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .AsSingleQuery() // Jawne u¿ycie Single Query
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Split Query - rozbicie na osobne zapytania SQL
    /// Eliminuje problem Cartesian Explosion
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> SplitQuery_MultipleIncludes()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .AsSplitQuery() // Rozbicie na osobne zapytania
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Split Query na poziomie DbContext
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> SplitQuery_ContextLevel()
    {
        using var context = CreateSplitQueryContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion

    #region G³êbokie zagnie¿d¿enie z kolekcjami

    /// <summary>
    /// Single Query z g³êbokim zagnie¿d¿eniem i wieloma kolekcjami
    /// Najbardziej podatne na Cartesian Explosion
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> SingleQuery_DeepNesting()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
                .ThenInclude(t => t!.Department)
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .Include(e => e.AuthoredReports)
            .AsSingleQuery()
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Split Query z g³êbokim zagnie¿d¿eniem
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> SplitQuery_DeepNesting()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
                .ThenInclude(t => t!.Department)
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .Include(e => e.AuthoredReports)
            .AsSplitQuery()
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion

    #region Porównanie z Projekcj¹ (najlepsza alternatywa)

    /// <summary>
    /// Projekcja jako alternatywa dla Include z kolekcjami
    /// Eliminuje problem Cartesian Explosion i pobiera tylko potrzebne dane
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeWithCollectionsDto>> Projection_Alternative()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeWithCollectionsDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                PositionName = e.Position.PositionName,
                TeamName = e.Team != null ? e.Team.TeamName : null,
                Absences = e.Absences.Select(a => new AbsenceDto
                {
                    AbsenceID = a.AbsenceID,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Status = a.Status.ToString()
                }).ToList(),
                DocumentsCount = e.Documents.Count
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion

    #region Test z filtrowaniem w kolekcjach

    /// <summary>
    /// Filtered Include - filtrowanie wewn¹trz Include (EF Core 5.0+)
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> FilteredInclude_SingleQuery()
    {
        using var context = CreateContext();
        var cutoffDate = DateTime.Now.AddYears(-1);
        
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Absences.Where(a => a.StartDate > cutoffDate))
            .AsSingleQuery()
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Filtered Include z Split Query
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> FilteredInclude_SplitQuery()
    {
        using var context = CreateContext();
        var cutoffDate = DateTime.Now.AddYears(-1);
        
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Absences.Where(a => a.StartDate > cutoffDate))
            .AsSplitQuery()
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion
}

#region DTO Classes

public class EmployeeWithCollectionsDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? PositionName { get; set; }
    public string? TeamName { get; set; }
    public List<AbsenceDto> Absences { get; set; } = new();
    public int DocumentsCount { get; set; }
}

public class AbsenceDto
{
    public int AbsenceID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
}

#endregion
