using BenchmarkDotNet.Attributes;
using HRManager.Benchmarks.Config;
using Microsoft.EntityFrameworkCore;

namespace HRManager.Benchmarks.Benchmarks;

/// <summary>
/// Scenariusz 1: Œledzenie zmian (Tracking vs. NoTracking)
/// 
/// Badanie narzutu wydajnoœciowego ChangeTrackera przy operacjach tylko do odczytu (read-only)
/// dla ma³ych (100) i du¿ych (10 000+) zbiorów danych.
/// 
/// Metryki: Czas wykonania, Alokacja pamiêci
/// </summary>
[Config(typeof(BenchmarkConfig))]
[MemoryDiagnoser]
public class TrackingVsNoTrackingBenchmarks : BenchmarkBase
{
    [Params(10, 100, 1000)]
    public int RecordCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        // Upewnij siê, ¿e baza danych istnieje i ma dane testowe
        using var context = CreateContext();
        context.Database.EnsureCreated();
        
        // SprawdŸ liczbê rekordów
        var count = context.Employees.Count();
        Console.WriteLine($"Liczba pracowników w bazie: {count}");
        
        // Jeœli potrzebujesz wiêcej danych testowych, mo¿esz je tutaj dodaæ
        // EnsureTestData(context, RecordCount);
    }

    /// <summary>
    /// Pobieranie z domyœlnym œledzeniem zmian (Tracking)
    /// ChangeTracker œledzi wszystkie pobrane encje
    /// </summary>
    [Benchmark(Baseline = true)]
    public async Task<List<HRManager.Models.Entities.Employee>> WithTracking()
    {
        using var context = CreateContext();
        return await context.Employees
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Pobieranie bez œledzenia zmian (NoTracking)
    /// Brak narzutu ChangeTrackera - szybsze dla operacji read-only
    /// </summary>
    [Benchmark]
    public async Task<List<HRManager.Models.Entities.Employee>> WithNoTracking()
    {
        using var context = CreateContext();
        return await context.Employees
            .AsNoTracking()
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// Pobieranie z NoTracking na poziomie DbContext
    /// </summary>
    [Benchmark]
    public async Task<List<HRManager.Models.Entities.Employee>> WithNoTrackingContext()
    {
        using var context = CreateNoTrackingContext();
        return await context.Employees
            .Take(RecordCount)
            .ToListAsync();
    }

    /// <summary>
    /// NoTracking z Include (eager loading bez œledzenia)
    /// </summary>
    [Benchmark]
    public async Task<List<HRManager.Models.Entities.Employee>> WithNoTracking_WithInclude()
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
    /// Tracking z Include (eager loading ze œledzeniem)
    /// </summary>
    [Benchmark]
    public async Task<List<HRManager.Models.Entities.Employee>> WithTracking_WithInclude()
    {
        using var context = CreateContext();
        return await context.Employees
            .Include(e => e.Position)
            .Include(e => e.Team)
            .Take(RecordCount)
            .ToListAsync();
    }
}
