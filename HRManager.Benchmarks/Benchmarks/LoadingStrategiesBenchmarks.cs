using BenchmarkDotNet.Attributes;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 2: Strategie ³adowania danych powi¹zanych
/// 
/// Porównanie wydajnoœci metod:
/// - Eager Loading (Include)
/// - Explicit Loading
/// - Select Loading (projekcja do DTO)
/// 
/// Cel: wykazanie ró¿nic w czasie i liczbie zapytañ SQL
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci, Liczba zapytañ SQL (do weryfikacji w MiniProfiler)
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class LoadingStrategiesBenchmarks : BenchmarkBase
{
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

    #region Pobieranie pojedynczego rekordu z relacjami

    /// <summary>
    /// Eager Loading - wszystkie relacje w jednym zapytaniu SQL (JOIN)
    /// Generuje 1 zapytanie SQL z wieloma JOIN
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<Employee?> EagerLoading_Single()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
                .ThenInclude(t => t!.Department)
            .Include(e => e.Photo)
            .Include(e => e.Absences)
            .FirstOrDefaultAsync(e => e.EmployeeID == _testEmployeeId);
    }

    /// <summary>
    /// Explicit Loading - osobne zapytania dla ka¿dej relacji
    /// Generuje N+1 zapytañ SQL (kontrolowane)
    /// </summary>
    [Benchmark]
    public async Task<Employee?> ExplicitLoading_Single()
    {
        using var context = CreateContext();
        var employee = await context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeID == _testEmployeeId);

        if (employee != null)
        {
            await context.Entry(employee).Reference(e => e.Position).LoadAsync();
            await context.Entry(employee).Reference(e => e.Team).LoadAsync();
            await context.Entry(employee).Reference(e => e.Photo).LoadAsync();
            await context.Entry(employee).Collection(e => e.Absences).LoadAsync();
            
            if (employee.Team != null)
            {
                await context.Entry(employee.Team).Reference(t => t.Department).LoadAsync();
            }
        }

        return employee;
    }

    /// <summary>
    /// Select Loading (Projekcja do DTO) - tylko potrzebne dane
    /// Najefektywniejsze - pobiera tylko wymagane kolumny
    /// </summary>
    [Benchmark]
    public async Task<EmployeeDetailsDto?> SelectLoading_Single()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Where(e => e.EmployeeID == _testEmployeeId)
            .Select(e => new EmployeeDetailsDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                Email = e.Email,
                PositionName = e.Position.PositionName,
                TeamName = e.Team != null ? e.Team.TeamName : null,
                DepartmentName = e.Team != null && e.Team.Department != null 
                    ? e.Team.Department.Name : null,
                AbsencesCount = e.Absences.Count
            })
            .FirstOrDefaultAsync();
    }

    #endregion

    #region Pobieranie wielu rekordów z relacjami

    [Params(10, 50, 100)]
    public int RecordCount { get; set; }

    /// <summary>
    /// Eager Loading - lista z Include
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> EagerLoading_List()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Select Loading (Projekcja) - lista DTO
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeListDto>> SelectLoading_List()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Select(e => new EmployeeListDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                Email = e.Email,
                PositionName = e.Position.PositionName,
                TeamName = e.Team != null ? e.Team.TeamName : null
            })
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Eager Loading z wieloma ThenInclude (g³êbokie zagnie¿d¿enie)
    /// </summary>
    [Benchmark]
    public async Task<List<Employee>> EagerLoading_Deep()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Include(e => e.Position)
            .Include(e => e.Team)
                .ThenInclude(t => t!.Department)
            .Include(e => e.Absences)
            .Take(RecordCount)
            .ToListAsync();
    }

    #endregion
}

#region DTO Classes

/// <summary>
/// DTO dla szczegó³ów pracownika
/// </summary>
public class EmployeeDetailsDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PositionName { get; set; }
    public string? TeamName { get; set; }
    public string? DepartmentName { get; set; }
    public int AbsencesCount { get; set; }
}

/// <summary>
/// DTO dla listy pracowników
/// </summary>
public class EmployeeListDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PositionName { get; set; }
    public string? TeamName { get; set; }
}

#endregion
