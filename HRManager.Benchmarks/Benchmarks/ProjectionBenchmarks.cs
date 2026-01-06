using BenchmarkDotNet.Attributes;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 3: Projekcja danych (Fetching full entities vs. Projections)
/// 
/// Porównanie pobierania pe³nych encji (wszystkie kolumny) vs pobieranie wybranych pól
/// bezpoœrednio w zapytaniu LINQ (.Select()).
/// 
/// Badanie wp³ywu na przepustowoœæ sieci i czas deserializacji.
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class ProjectionBenchmarks : BenchmarkBase
{
    [Params(10, 100, 500)]
    public int RecordCount { get; set; }

    #region Porównanie: Pe³ne encje vs Projekcja

    /// <summary>
    /// Pobieranie pe³nych encji Employee (wszystkie kolumny)
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<List<Employee>> FullEntity()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Projekcja - tylko ID i imiê (minimalny DTO)
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeMinimalDto>> Projection_Minimal()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeMinimalDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Projekcja - podstawowe dane (œredni DTO)
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeBasicDto>> Projection_Basic()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeBasicDto
            {
                EmployeeID = e.EmployeeID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Projekcja - dane z relacjami (rozbudowany DTO)
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeFullDto>> Projection_WithRelations()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeFullDto
            {
                EmployeeID = e.EmployeeID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                City = e.City,
                PositionName = e.Position.PositionName,
                TeamName = e.Team != null ? e.Team.TeamName : null,
                DepartmentName = e.Team != null && e.Team.Department != null 
                    ? e.Team.Department.Name : null
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Pe³ne encje z Include (wszystkie kolumny + relacje)
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> FullEntity_WithIncludes()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
                .ThenInclude(t => t!.Department)
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion

    #region Projekcja z agregacj¹

    /// <summary>
    /// Projekcja z prost¹ agregacj¹ (Count)
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeWithStatsDto>> Projection_WithAggregation()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeWithStatsDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                AbsencesCount = e.Absences.Count,
                DocumentsCount = e.Documents.Count,
                ReportsAuthoredCount = e.AuthoredReports.Count
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Pobieranie pe³nych encji + relacje kolekcji (dla porównania z agregacj¹)
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeWithStatsDto>> FullEntity_ManualAggregation()
    {
        using var context = CreateContext();
        var employees = await context.Employees
            .AsNoTracking()
            .Include(e => e.Absences)
            .Include(e => e.Documents)
            .Include(e => e.AuthoredReports)
            .Take(RecordCount)
            .ToListAsync();

        return employees.Select(e => new EmployeeWithStatsDto
        {
            EmployeeID = e.EmployeeID,
            FullName = e.FirstName + " " + e.LastName,
            AbsencesCount = e.Absences?.Count ?? 0,
            DocumentsCount = e.Documents?.Count ?? 0,
            ReportsAuthoredCount = e.AuthoredReports?.Count ?? 0
        }).ToList();
    }

    #endregion

    #region Typy anonimowe vs klasy DTO

    /// <summary>
    /// Projekcja do typu anonimowego
    /// </summary>
    [Benchmark]
    public async Task<object> Projection_Anonymous()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new
            {
                e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                e.Email,
                Position = e.Position.PositionName
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion
}

#region DTO Classes

public class EmployeeMinimalDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
}

public class EmployeeBasicDto
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class EmployeeFullDto
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? PositionName { get; set; }
    public string? TeamName { get; set; }
    public string? DepartmentName { get; set; }
}

public class EmployeeWithStatsDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int AbsencesCount { get; set; }
    public int DocumentsCount { get; set; }
    public int ReportsAuthoredCount { get; set; }
}

#endregion
