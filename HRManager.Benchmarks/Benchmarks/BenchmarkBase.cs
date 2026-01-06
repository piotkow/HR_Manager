using HRManager.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Klasa bazowa dla wszystkich benchmarków
/// Zawiera konfiguracjê po³¹czenia z baz¹ danych
/// </summary>
public abstract class BenchmarkBase
{
    protected const string ConnectionString = 
        @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HRManager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    /// <summary>
    /// Tworzy nowy DbContext z domyœlnymi ustawieniami
    /// </summary>
    protected HRManagerDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<HRManagerDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;
        return new HRManagerDbContext(options);
    }

    /// <summary>
    /// Tworzy nowy DbContext bez œledzenia zmian (NoTracking)
    /// </summary>
    protected HRManagerDbContext CreateNoTrackingContext()
    {
        var options = new DbContextOptionsBuilder<HRManagerDbContext>()
            .UseSqlServer(ConnectionString)
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .Options;
        return new HRManagerDbContext(options);
    }

    /// <summary>
    /// Tworzy nowy DbContext z Split Query jako domyœlnym
    /// </summary>
    protected HRManagerDbContext CreateSplitQueryContext()
    {
        var options = new DbContextOptionsBuilder<HRManagerDbContext>()
            .UseSqlServer(ConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .Options;
        return new HRManagerDbContext(options);
    }

    /// <summary>
    /// Tworzy po³¹czenie SQL dla Dapper i ADO.NET
    /// </summary>
    protected SqlConnection CreateSqlConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}
