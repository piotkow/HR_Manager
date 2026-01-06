using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using HRManager.Benchmarks.Benchmarks;

// Uruchomienie benchmarków
// W trybie Debug użyj konfiguracji DebugInProcess dla szybkich testów
// W trybie Release uruchom pełne benchmarki

#if DEBUG
// Tryb Debug - szybkie testy bez pełnego rozgrzewania
Console.WriteLine("=== TRYB DEBUG - Szybkie testy ===");
Console.WriteLine("Dla pełnych wyników uruchom w trybie Release!");
Console.WriteLine();

// Możesz odkomentować scenariusz który chcesz przetestować:
// BenchmarkRunner.Run<TrackingVsNoTrackingBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<LoadingStrategiesBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<ProjectionBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<SplitQueryBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<BulkOperationsBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<SimpleQueryComparisonBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<ComplexQueryComparisonBenchmarks>(new DebugInProcessConfig());
// BenchmarkRunner.Run<HighPerformanceInsertBenchmarks>(new DebugInProcessConfig());

Console.WriteLine("Odkomentuj wybrany benchmark w Program.cs");
#else
// Tryb Release - wybierz benchmarki do uruchomienia
var switcher = new BenchmarkSwitcher(new[]
{
    // Grupa A: Wewnętrzna optymalizacja EF Core
    typeof(TrackingVsNoTrackingBenchmarks),     // Scenariusz 1
    typeof(LoadingStrategiesBenchmarks),         // Scenariusz 2
    typeof(ProjectionBenchmarks),                // Scenariusz 3
    typeof(SplitQueryBenchmarks),                // Scenariusz 4
    typeof(BulkOperationsBenchmarks),            // Scenariusz 5
    
    // Grupa B: Porównanie EF Core vs Dapper vs ADO.NET
    typeof(SimpleQueryComparisonBenchmarks),     // Scenariusz 6
    typeof(ComplexQueryComparisonBenchmarks),    // Scenariusz 7
    typeof(HighPerformanceInsertBenchmarks)      // Scenariusz 8
});

switcher.Run(args);
#endif
