using BenchmarkDotNet.Attributes;
using Dapper;
using HRManager.Benchmarks.Config;
using HRManager.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 6: Prosty odczyt (Simple Query) - GetById
/// 
/// Porównanie optymalnie skonfigurowanego EF Core z Dapper i ADO.NET
/// przy pobieraniu pojedynczego rekordu po kluczu g³ównym.
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class SimpleQueryComparisonBenchmarks : BenchmarkBase
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

    #region EF Core Variants

    /// <summary>
    /// EF Core - Find (u¿ywa cache'u ChangeTrackera jeœli istnieje)
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<Employee?> EFCore_Find()
    {
        using var context = CreateContext();
        return await context.Employees.FindAsync(_testEmployeeId);
    }

    /// <summary>
    /// EF Core - FirstOrDefault z NoTracking
    /// </summary>
    [Benchmark]
    public async Task<Employee?> EFCore_FirstOrDefault_NoTracking()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmployeeID == _testEmployeeId);
    }

    /// <summary>
    /// EF Core - SingleOrDefault z NoTracking
    /// </summary>
    [Benchmark]
    public async Task<Employee?> EFCore_SingleOrDefault_NoTracking()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.EmployeeID == _testEmployeeId);
    }

    /// <summary>
    /// EF Core - Projekcja do DTO
    /// </summary>
    [Benchmark]
    public async Task<SimpleEmployeeDto?> EFCore_Projection()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Where(e => e.EmployeeID == _testEmployeeId)
            .Select(e => new SimpleEmployeeDto
            {
                EmployeeID = e.EmployeeID,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// EF Core - Raw SQL
    /// </summary>
    [Benchmark]
    public async Task<Employee?> EFCore_RawSql()
    {
        using var context = CreateContext();
        return await context.Employees
            .FromSqlInterpolated($"SELECT * FROM Employees WHERE EmployeeID = {_testEmployeeId}")
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    #endregion

    #region Dapper

    /// <summary>
    /// Dapper - Query z mapowaniem do encji
    /// </summary>
    [Benchmark]
    public async Task<Employee?> Dapper_Query()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();
        
        return await connection.QueryFirstOrDefaultAsync<Employee>(
            "SELECT * FROM Employees WHERE EmployeeID = @Id",
            new { Id = _testEmployeeId });
    }

    /// <summary>
    /// Dapper - Query do DTO
    /// </summary>
    [Benchmark]
    public async Task<SimpleEmployeeDto?> Dapper_QueryDto()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();
        
        return await connection.QueryFirstOrDefaultAsync<SimpleEmployeeDto>(
            "SELECT EmployeeID, FirstName, LastName, Email FROM Employees WHERE EmployeeID = @Id",
            new { Id = _testEmployeeId });
    }

    /// <summary>
    /// Dapper - QuerySingle (rzuca wyj¹tek gdy brak rekordu)
    /// </summary>
    [Benchmark]
    public async Task<Employee?> Dapper_QuerySingle()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();
        
        try
        {
            return await connection.QuerySingleOrDefaultAsync<Employee>(
                "SELECT * FROM Employees WHERE EmployeeID = @Id",
                new { Id = _testEmployeeId });
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ADO.NET

    /// <summary>
    /// ADO.NET - DataReader z rêcznym mapowaniem
    /// </summary>
    [Benchmark]
    public async Task<Employee?> AdoNet_DataReader()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(
            "SELECT * FROM Employees WHERE EmployeeID = @Id", connection);
        command.Parameters.AddWithValue("@Id", _testEmployeeId);

        using var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            return MapEmployeeFromReader(reader);
        }
        
        return null;
    }

    /// <summary>
    /// ADO.NET - DataReader do DTO
    /// </summary>
    [Benchmark]
    public async Task<SimpleEmployeeDto?> AdoNet_DataReaderDto()
    {
        using var connection = CreateSqlConnection();
        await connection.OpenAsync();

        using var command = new SqlCommand(
            "SELECT EmployeeID, FirstName, LastName, Email FROM Employees WHERE EmployeeID = @Id", 
            connection);
        command.Parameters.AddWithValue("@Id", _testEmployeeId);

        using var reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
        {
            return new SimpleEmployeeDto
            {
                EmployeeID = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3)
            };
        }
        
        return null;
    }

    private static Employee MapEmployeeFromReader(SqlDataReader reader)
    {
        return new Employee
        {
            EmployeeID = reader.GetInt32(reader.GetOrdinal("EmployeeID")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
            Phone = reader.GetString(reader.GetOrdinal("Phone")),
            Country = reader.GetString(reader.GetOrdinal("Country")),
            City = reader.GetString(reader.GetOrdinal("City")),
            Street = reader.GetString(reader.GetOrdinal("Street")),
            PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
            DateOfEmployment = reader.GetDateTime(reader.GetOrdinal("DateOfEmployment")),
            PositionID = reader.GetInt32(reader.GetOrdinal("PositionID")),
            TeamID = reader.IsDBNull(reader.GetOrdinal("TeamID")) 
                ? null 
                : reader.GetInt32(reader.GetOrdinal("TeamID"))
        };
    }

    #endregion
}

#region DTO

public class SimpleEmployeeDto
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

#endregion
