using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;

namespace HRManager.Benchmarks.Config;

/// <summary>
/// Konfiguracja benchmarków z metrykami:
/// - Czas wykonania (Execution Time)
/// - Alokacja pamiêci (Memory Allocation)
/// 
/// Eksporty dla pracy magisterskiej:
/// - CSV (do analizy w Excel/Python)
/// - JSON (dane strukturalne)
/// - HTML (raporty)
/// - Markdown (do dokumentacji)
/// - R Plots (wykresy - wymaga zainstalowanego R)
/// </summary>
public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        // Job konfiguracja - œrednia iloœæ iteracji
        AddJob(Job.Default
            .WithWarmupCount(3)      // Liczba rozgrzewek
            .WithIterationCount(10)  // Liczba iteracji pomiarowych
            .WithInvocationCount(16) // Liczba wywo³añ na iteracjê
        );

        // Diagnostyka pamiêci - Memory Allocation
        AddDiagnoser(MemoryDiagnoser.Default);

        // Kolumny wyników
        AddColumn(StatisticColumn.Mean);
        AddColumn(StatisticColumn.StdDev);
        AddColumn(StatisticColumn.Median);
        AddColumn(StatisticColumn.Min);
        AddColumn(StatisticColumn.Max);

        // === EKSPORTERY DLA PRACY MAGISTERSKIEJ ===
        
        // CSV - do analizy w Excel lub Python (pandas)
        AddExporter(CsvExporter.Default);
        AddExporter(new CsvMeasurementsExporter(CsvSeparator.Semicolon)); // Szczegó³owe pomiary
        
        // JSON - dane strukturalne do w³asnych wykresów
        AddExporter(JsonExporter.Full);
        AddExporter(JsonExporter.FullCompressed);
        
        // HTML - czytelne raporty
        AddExporter(HtmlExporter.Default);
        
        // Markdown - do dokumentacji/GitHub
        AddExporter(MarkdownExporter.GitHub);
        
        // R Plots - automatyczne wykresy (wymaga R)
        // Instalacja R: https://www.r-project.org/
        // Po instalacji uruchom: Rscript BuildPlots.R
        AddExporter(RPlotExporter.Default);

        // Logger
        AddLogger(ConsoleLogger.Default);

        // Sortowanie wyników
        WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));
    }
}

/// <summary>
/// Konfiguracja z pe³nymi wykresami i dodatkowymi statystykami
/// U¿ywaj dla finalnych pomiarów do pracy magisterskiej
/// </summary>
public class ThesisConfig : ManualConfig
{
    public ThesisConfig()
    {
        // Wiêcej iteracji dla dok³adniejszych wyników
        AddJob(Job.Default
            .WithWarmupCount(5)       // Wiêcej rozgrzewek
            .WithIterationCount(20)   // Wiêcej iteracji
            .WithInvocationCount(32)  // Wiêcej wywo³añ
        );

        // Pe³na diagnostyka
        AddDiagnoser(MemoryDiagnoser.Default);

        // Wszystkie kolumny statystyczne
        AddColumn(StatisticColumn.Mean);
        AddColumn(StatisticColumn.StdErr);
        AddColumn(StatisticColumn.StdDev);
        AddColumn(StatisticColumn.Median);
        AddColumn(StatisticColumn.Min);
        AddColumn(StatisticColumn.Max);
        AddColumn(StatisticColumn.P95);
        AddColumn(StatisticColumn.Iterations);

        // Wszystkie eksportery
        AddExporter(CsvExporter.Default);
        AddExporter(new CsvMeasurementsExporter(CsvSeparator.Semicolon));
        AddExporter(JsonExporter.Full);
        AddExporter(HtmlExporter.Default);
        AddExporter(MarkdownExporter.GitHub);
        AddExporter(RPlotExporter.Default);

        AddLogger(ConsoleLogger.Default);
        WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));
    }
}

/// <summary>
/// Szybka konfiguracja do testów w trybie Debug
/// </summary>
public class QuickBenchmarkConfig : ManualConfig
{
    public QuickBenchmarkConfig()
    {
        AddJob(Job.Dry);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddLogger(ConsoleLogger.Default);
    }
}
