using BenchmarkDotNet.Attributes;
using Dapper;
using HRManager.Benchmarks.Config;
using HRManager.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 7: Raportowanie (Complex Query)
/// 
/// Skomplikowane zapytanie agreguj¹ce dane z kilku tabel:
/// - Z³¹czenia (JOIN)
/// - Filtrowanie
/// - Sortowanie
/// - Grupowanie i agregacje
/// 
/// Porównanie: EF Core (LINQ) vs Dapper (Raw SQL) vs ADO.NET
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci, Jakoœæ SQL
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class ComplexQueryComparisonBenchmarks : BenchmarkBase
{
    #region Raport: Pracownicy z nieobecnoœciami w danym okresie

    /// <summary>
    /// EF Core - zapytanie z wieloma z³¹czeniami i filtrowaniem
    /// U¿ywa SqlServerDbFunctionsExtensions.DateDiffDay dla obliczenia ró¿nicy dni
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<List<EmployeeAbsenceReportDto>> EFCore_ComplexQuery()
    {
        using var context = CreateContext();
        var startDate = DateTime.Now.AddYears(-1);
        var endDate = DateTime.Now;

        return await context.Employees
            .AsNoTracking()
            .Where(e => e.Absences.Any(a => a.StartDate >= startDate && a.EndDate <= endDate))
            .Select(e => new EmployeeAbsenceReportDto
            {
                EmployeeID = e.EmployeeID,
                FullName = e.FirstName + " " + e.LastName,
                Email = e.Email,
                PositionName = e.Position.PositionName,
                TeamName = e.Team != null ? e.Team.TeamName : null,
                DepartmentName = e.Team != null && e.Team.Department != null 
                    ? e.Team.Department.Name : null,
                TotalAbsences = e.Absences.Count(a => a.StartDate >= startDate && a.EndDate <= endDate),
                ApprovedAbsences = e.Absences.Count(a => 
                    a.StartDate >= startDate && a.EndDate <= endDate && a.Status == Status.Approved),
                // U¿yj jawnie SQL Server DateDiffDay
                TotalAbsenceDays = e.Absences
                    .Where(a => a.StartDate >= startDate && a.EndDate <= endDate)
                    .Sum(a => SqlServerDbFunctionsExtensions.DateDiffDay(EF.Functions, a.StartDate, a.EndDate))
            })
            .OrderByDescending(e => e.TotalAbsences)
            .ToListAsync();
    }

    /// <summary>
    /// EF Core - u¿ycie Raw SQL z FromSqlRaw
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeAbsenceReportDto>> EFCore_RawSql()
    {
        using var context = CreateContext();
        var startDate = DateTime.Now.AddYears(-1);
        var endDate = DateTime.Now;

        var sql = @"
            SELECT 
                e.EmployeeID,
                e.FirstName + ' ' + e.LastName AS FullName,
                e.Email,
                p.PositionName,
                t.TeamName,
                d.Name AS DepartmentName,
                COUNT(a.AbsenceID) AS TotalAbsences,
                SUM(CASE WHEN a.Status = 1 THEN 1 ELSE 0 END) AS ApprovedAbsences,
                ISNULL(SUM(DATEDIFF(DAY, a.StartDate, a.EndDate)), 0) AS TotalAbsenceDays
            FROM Employees e
            INNER JOIN Positions p ON e.PositionID = p.PositionID
            LEFT JOIN Teams t ON e.TeamID = t.TeamID
            LEFT JOIN Departments d ON t.DepartmentID = d.DerpartmentID
            LEFT JOIN Absences a ON e.EmployeeID = a.EmployeeID 
                AND a.StartDate >= @StartDate AND a.EndDate <= @EndDate
            GROUP BY e.EmployeeID, e.FirstName, e.LastName, e.Email, p.PositionName, t.TeamName, d.Name
            HAVING COUNT(a.AbsenceID) > 0
            ORDER BY TotalAbsences DESC";

        return await context.Database
            .SqlQueryRaw<EmployeeAbsenceReportDto>(sql, 
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate))
            .ToListAsync();
    }

    /// <summary>
    /// Dapper - Raw SQL
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeAbsenceReportDto>> Dapper_ComplexQuery()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var startDate = DateTime.Now.AddYears(-1);
        var endDate = DateTime.Now;

        var sql = @"
            SELECT 
                e.EmployeeID,
                e.FirstName + ' ' + e.LastName AS FullName,
                e.Email,
                p.PositionName,
                t.TeamName,
                d.Name AS DepartmentName,
                COUNT(a.AbsenceID) AS TotalAbsences,
                SUM(CASE WHEN a.Status = 1 THEN 1 ELSE 0 END) AS ApprovedAbsences,
                ISNULL(SUM(DATEDIFF(DAY, a.StartDate, a.EndDate)), 0) AS TotalAbsenceDays
            FROM Employees e
            INNER JOIN Positions p ON e.PositionID = p.PositionID
            LEFT JOIN Teams t ON e.TeamID = t.TeamID
            LEFT JOIN Departments d ON t.DepartmentID = d.DerpartmentID
            LEFT JOIN Absences a ON e.EmployeeID = a.EmployeeID 
                AND a.StartDate >= @StartDate AND a.EndDate <= @EndDate
            GROUP BY e.EmployeeID, e.FirstName, e.LastName, e.Email, p.PositionName, t.TeamName, d.Name
            HAVING COUNT(a.AbsenceID) > 0
            ORDER BY TotalAbsences DESC";

        var result = await connection.QueryAsync<EmployeeAbsenceReportDto>(sql, 
            new { StartDate = startDate, EndDate = endDate });
        
        return result.ToList();
    }

    /// <summary>
    /// ADO.NET - Raw SQL z DataReader
    /// </summary>
    [Benchmark]
    public async Task<List<EmployeeAbsenceReportDto>> AdoNet_ComplexQuery()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var startDate = DateTime.Now.AddYears(-1);
        var endDate = DateTime.Now;

        var sql = @"
            SELECT 
                e.EmployeeID,
                e.FirstName + ' ' + e.LastName AS FullName,
                e.Email,
                p.PositionName,
                t.TeamName,
                d.Name AS DepartmentName,
                COUNT(a.AbsenceID) AS TotalAbsences,
                SUM(CASE WHEN a.Status = 1 THEN 1 ELSE 0 END) AS ApprovedAbsences,
                ISNULL(SUM(DATEDIFF(DAY, a.StartDate, a.EndDate)), 0) AS TotalAbsenceDays
            FROM Employees e
            INNER JOIN Positions p ON e.PositionID = p.PositionID
            LEFT JOIN Teams t ON e.TeamID = t.TeamID
            LEFT JOIN Departments d ON t.DepartmentID = d.DerpartmentID
            LEFT JOIN Absences a ON e.EmployeeID = a.EmployeeID 
                AND a.StartDate >= @StartDate AND a.EndDate <= @EndDate
            GROUP BY e.EmployeeID, e.FirstName, e.LastName, e.Email, p.PositionName, t.TeamName, d.Name
            HAVING COUNT(a.AbsenceID) > 0
            ORDER BY TotalAbsences DESC";

        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StartDate", startDate);
        command.Parameters.AddWithValue("@EndDate", endDate);

        var results = new List<EmployeeAbsenceReportDto>();
        using var reader = await command.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            results.Add(new EmployeeAbsenceReportDto
            {
                EmployeeID = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                PositionName = reader.IsDBNull(3) ? null : reader.GetString(3),
                TeamName = reader.IsDBNull(4) ? null : reader.GetString(4),
                DepartmentName = reader.IsDBNull(5) ? null : reader.GetString(5),
                TotalAbsences = reader.GetInt32(6),
                ApprovedAbsences = reader.GetInt32(7),
                TotalAbsenceDays = reader.GetInt32(8)
            });
        }

        return results;
    }

    #endregion

    #region Raport: Statystyki zespo³ów

    /// <summary>
    /// EF Core - agregacja z grupowaniem
    /// </summary>
    [Benchmark]
    public async Task<List<TeamStatsDto>> EFCore_GroupByQuery()
    {
        using var context = CreateContext();

        return await context.Teams
            .AsNoTracking()
            .Select(t => new TeamStatsDto
            {
                TeamID = t.TeamID,
                TeamName = t.TeamName,
                DepartmentName = t.Department != null ? t.Department.Name : null,
                EmployeeCount = t.Employees.Count,
                TotalAbsences = t.Employees.SelectMany(e => e.Absences).Count(),
                TotalDocuments = t.Employees.SelectMany(e => e.Documents).Count()
            })
            .OrderByDescending(t => t.EmployeeCount)
            .ToListAsync();
    }

    /// <summary>
    /// Dapper - agregacja z grupowaniem
    /// </summary>
    [Benchmark]
    public async Task<List<TeamStatsDto>> Dapper_GroupByQuery()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        var sql = @"
            SELECT 
                t.TeamID,
                t.TeamName,
                d.Name AS DepartmentName,
                COUNT(DISTINCT e.EmployeeID) AS EmployeeCount,
                COUNT(a.AbsenceID) AS TotalAbsences,
                COUNT(doc.DocumentID) AS TotalDocuments
            FROM Teams t
            LEFT JOIN Departments d ON t.DepartmentID = d.DerpartmentID
            LEFT JOIN Employees e ON t.TeamID = e.TeamID
            LEFT JOIN Absences a ON e.EmployeeID = a.EmployeeID
            LEFT JOIN Documents doc ON e.EmployeeID = doc.EmployeeID
            GROUP BY t.TeamID, t.TeamName, d.Name
            ORDER BY EmployeeCount DESC";

        var result = await connection.QueryAsync<TeamStatsDto>(sql);
        return result.ToList();
    }

    #endregion
}

#region DTO Classes

public class EmployeeAbsenceReportDto
{
    public int EmployeeID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PositionName { get; set; }
    public string? TeamName { get; set; }
    public string? DepartmentName { get; set; }
    public int TotalAbsences { get; set; }
    public int ApprovedAbsences { get; set; }
    public int TotalAbsenceDays { get; set; }
}

public class TeamStatsDto
{
    public int TeamID { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string? DepartmentName { get; set; }
    public int EmployeeCount { get; set; }
    public int TotalAbsences { get; set; }
    public int TotalDocuments { get; set; }
}

#endregion
