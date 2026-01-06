using HRManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;
using System.Diagnostics;

namespace HRManager.Benchmarks.Helpers;

/// <summary>
/// Pomocnicze narzêdzia do diagnostyki zapytañ SQL.
/// 
/// Dla pe³nej integracji z MiniProfiler w projekcie API:
/// 
/// 1. Dodaj pakiet do HRManager.Api:
///    dotnet add package MiniProfiler.AspNetCore.Mvc
///    dotnet add package MiniProfiler.EntityFrameworkCore
/// 
/// 2. Skonfiguruj w Program.cs:
///    services.AddMiniProfiler(options => 
///    {
///        options.RouteBasePath = "/profiler";
///        options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
///    }).AddEntityFramework();
/// 
/// 3. U¿yj middleware:
///    app.UseMiniProfiler();
/// 
/// 4. Dostêp do wyników: /profiler/results-index
/// </summary>
public static class SqlDiagnosticsHelper
{
    /// <summary>
    /// Tworzy DbContext z logowaniem zapytañ SQL do konsoli.
    /// U¿yj do analizy generowanych zapytañ podczas developmentu.
    /// </summary>
    public static HRManagerDbContext CreateContextWithLogging(string connectionString)
    {
        var options = new DbContextOptionsBuilder<HRManagerDbContext>()
            .UseSqlServer(connectionString)
            .LogTo(message => Debug.WriteLine(message), 
                   new[] { DbLoggerCategory.Database.Command.Name })
            .EnableSensitiveDataLogging()
            .Options;

        return new HRManagerDbContext(options);
    }

    /// <summary>
    /// Tworzy DbContext z interceptorem do zliczania zapytañ.
    /// </summary>
    public static (HRManagerDbContext Context, QueryCountingInterceptor Counter) CreateContextWithQueryCounter(string connectionString)
    {
        var counter = new QueryCountingInterceptor();
        
        var options = new DbContextOptionsBuilder<HRManagerDbContext>()
            .UseSqlServer(connectionString)
            .AddInterceptors(counter)
            .Options;

        return (new HRManagerDbContext(options), counter);
    }
}

/// <summary>
/// Interceptor zliczaj¹cy zapytania SQL.
/// Przydatny do wykrywania problemu N+1.
/// </summary>
public class QueryCountingInterceptor : DbCommandInterceptor
{
    private int _queryCount;
    private readonly List<string> _queries = new();

    public int QueryCount => _queryCount;
    public IReadOnlyList<string> Queries => _queries.AsReadOnly();

    public override InterceptionResult<int> NonQueryExecuting(
        DbCommand command, 
        CommandEventData eventData, 
        InterceptionResult<int> result)
    {
        RecordQuery(command);
        return base.NonQueryExecuting(command, eventData, result);
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command, 
        CommandEventData eventData, 
        InterceptionResult<DbDataReader> result)
    {
        RecordQuery(command);
        return base.ReaderExecuting(command, eventData, result);
    }

    public override InterceptionResult<object> ScalarExecuting(
        DbCommand command, 
        CommandEventData eventData, 
        InterceptionResult<object> result)
    {
        RecordQuery(command);
        return base.ScalarExecuting(command, eventData, result);
    }

    private void RecordQuery(DbCommand command)
    {
        Interlocked.Increment(ref _queryCount);
        lock (_queries)
        {
            _queries.Add(command.CommandText);
        }
    }

    public void Reset()
    {
        _queryCount = 0;
        lock (_queries)
        {
            _queries.Clear();
        }
    }

    public void PrintSummary()
    {
        Console.WriteLine($"=== SQL Query Summary ===");
        Console.WriteLine($"Total queries executed: {_queryCount}");
        Console.WriteLine();
        
        for (int i = 0; i < _queries.Count; i++)
        {
            Console.WriteLine($"Query {i + 1}:");
            Console.WriteLine(_queries[i]);
            Console.WriteLine();
        }
    }
}

/// <summary>
/// Przyk³ad u¿ycia QueryCountingInterceptor do wykrywania N+1:
/// 
/// var (context, counter) = SqlDiagnosticsHelper.CreateContextWithQueryCounter(connectionString);
/// 
/// // Wykonaj operacjê
/// var employees = await context.Employees.ToListAsync();
/// foreach (var emp in employees)
/// {
///     var team = emp.Team; // Lazy loading - powoduje N+1!
/// }
/// 
/// counter.PrintSummary();
/// // Output: Total queries executed: N+1 (1 dla pracowników + N dla zespo³ów)
/// 
/// // Rozwi¹zanie - u¿yj Include:
/// var employees = await context.Employees.Include(e => e.Team).ToListAsync();
/// </summary>
public class NPlusOneDetectionExample
{
    // Placeholder dla dokumentacji
}
