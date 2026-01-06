# HRManager Benchmarks

## Przegl¹d

Ten projekt zawiera testy wydajnoœciowe (benchmarks) dla ró¿nych scenariuszy operacji bazodanowych z wykorzystaniem Entity Framework Core.

## Wymagania

- .NET 8.0
- SQL Server LocalDB (lub zmieñ ConnectionString w `BenchmarkBase.cs`)
- Baza danych HRManager z danymi testowymi
- Python 3.8+ (dla wykresów) lub R 4.0+ (alternatywnie)

## Struktura projektu

```
HRManager.Benchmarks/
??? Config/
?   ??? BenchmarkConfig.cs          # Konfiguracja BenchmarkDotNet
??? Benchmarks/
?   ??? BenchmarkBase.cs            # Klasa bazowa z po³¹czeniem DB
?   ??? TrackingVsNoTrackingBenchmarks.cs    # Scenariusz 1
?   ??? LoadingStrategiesBenchmarks.cs        # Scenariusz 2
?   ??? ProjectionBenchmarks.cs               # Scenariusz 3
?   ??? SplitQueryBenchmarks.cs               # Scenariusz 4
?   ??? BulkOperationsBenchmarks.cs           # Scenariusz 5
?   ??? SimpleQueryComparisonBenchmarks.cs    # Scenariusz 6
?   ??? ComplexQueryComparisonBenchmarks.cs   # Scenariusz 7
?   ??? HighPerformanceInsertBenchmarks.cs    # Scenariusz 8
??? generate_charts.py              # Skrypt do generowania wykresów
??? r_plots_guide.R                 # Przewodnik po wykresach R
??? Program.cs
```

## Scenariusze testowe

### Grupa A: Wewnêtrzna optymalizacja EF Core

1. **Tracking vs NoTracking** - Badanie narzutu ChangeTrackera
2. **Strategie ³adowania** - Eager/Explicit/Select Loading
3. **Projekcja danych** - Pe³ne encje vs DTO
4. **Split Queries** - Cartesian Explosion vs Query Splitting
5. **Operacje masowe** - AddRange vs Bulk Extensions

### Grupa B: EF Core vs Dapper vs ADO.NET

6. **Prosty odczyt** - GetById
7. **Raportowanie** - Skomplikowane zapytania agreguj¹ce
8. **Wysokowydajny zapis** - Masowe wstawianie danych

## Uruchamianie benchmarków

### Tryb Release (pe³ne benchmarki)

```powershell
cd HRManager.Benchmarks
dotnet run -c Release
```

Po uruchomieniu pojawi siê menu wyboru benchmarków.

### Uruchomienie konkretnego benchmarku

```powershell
dotnet run -c Release -- --filter *TrackingVsNoTracking*
dotnet run -c Release -- --filter *LoadingStrategies*
dotnet run -c Release -- --filter *Projection*
dotnet run -c Release -- --filter *SplitQuery*
dotnet run -c Release -- --filter *BulkOperations*
dotnet run -c Release -- --filter *SimpleQueryComparison*
dotnet run -c Release -- --filter *ComplexQueryComparison*
dotnet run -c Release -- --filter *HighPerformanceInsert*
```

### Uruchomienie wszystkich benchmarków

```powershell
dotnet run -c Release -- --filter *
```

### Konfiguracja dla pracy magisterskiej (dok³adniejsze wyniki)

W pliku benchmarków zmieñ atrybut na:
```csharp
[Config(typeof(ThesisConfig))]  // Zamiast BenchmarkConfig
```

## ?? Generowanie wykresów

### Opcja 1: Python (zalecane)

```powershell
# Zainstaluj wymagane pakiety
pip install pandas matplotlib seaborn

# Wygeneruj wykresy
cd HRManager.Benchmarks
python generate_charts.py
```

Wykresy zostan¹ zapisane w folderze `Charts/`:
- `execution_time.png` - wykres s³upkowy czasu wykonania
- `memory_allocation.png` - wykres alokacji pamiêci
- `comparison.png` - wykres porównawczy (czas + pamiêæ)
- `speedup.png` - wykres przyspieszenia wzglêdem baseline
- `grouped_by_*.png` - wykresy dla ró¿nych parametrów

### Opcja 2: R (automatyczne przez BenchmarkDotNet)

```powershell
# Zainstaluj R z https://www.r-project.org/

# Po uruchomieniu benchmarków
cd BenchmarkDotNet.Artifacts/results
Rscript BuildPlots.R
```

Wykresy PNG zostan¹ wygenerowane automatycznie.

### Opcja 3: Excel/Google Sheets

U¿yj plików CSV z folderu `BenchmarkDotNet.Artifacts/results/`:
- `*-report.csv` - podsumowanie wyników
- `*-measurements.csv` - szczegó³owe pomiary

## Wyniki i eksporty

Wyniki s¹ eksportowane do `BenchmarkDotNet.Artifacts/results/`:

| Format | Plik | Zastosowanie |
|--------|------|--------------|
| CSV | `*-report.csv` | Analiza w Excel/Python |
| CSV | `*-measurements.csv` | Szczegó³owe pomiary |
| JSON | `*-full.json` | Dane strukturalne |
| HTML | `*-report.html` | Czytelne raporty |
| Markdown | `*-github.md` | Dokumentacja/GitHub |
| R | `BuildPlots.R` | Wykresy w R |

## Metryki pomiarowe

Ka¿dy benchmark mierzy:

| Metryka | Opis |
|---------|------|
| **Mean** | Œredni czas wykonania |
| **StdDev** | Odchylenie standardowe |
| **Median** | Mediana |
| **Min/Max** | Wartoœci skrajne |
| **P95** | 95. percentyl (ThesisConfig) |
| **Allocated** | Pamiêæ na stercie |
| **Gen0/Gen1** | Kolekcje GC |

## Integracja z MiniProfiler

Dla dodatkowej analizy zapytañ SQL (liczba roundtrips, plany wykonania), 
skonfiguruj MiniProfiler w projekcie API:

```powershell
# W projekcie HRManager.Api
dotnet add package MiniProfiler.AspNetCore.Mvc
dotnet add package MiniProfiler.EntityFrameworkCore
```

Konfiguracja w `Program.cs`:
```csharp
builder.Services.AddMiniProfiler(options => 
{
    options.RouteBasePath = "/profiler";
}).AddEntityFramework();

// W pipeline
app.UseMiniProfiler();
```

Dostêp do wyników: `http://localhost:5196/profiler/results-index`

## Wskazówki dla pracy magisterskiej

1. **Dane testowe** - Upewnij siê, ¿e baza danych zawiera wystarczaj¹c¹ iloœæ danych (100-10000 rekordów)
2. **Izolacja** - Zamknij inne aplikacje podczas benchmarków
3. **Powtarzalnoœæ** - Uruchom benchmarki kilka razy dla weryfikacji wyników
4. **ThesisConfig** - U¿yj konfiguracji `ThesisConfig` dla finalnych pomiarów
5. **Œrodowisko** - Dokumentuj specyfikacjê sprzêtu i wersje oprogramowania

## Pakiety NuGet

- BenchmarkDotNet 0.14.0
- Dapper 2.1.35
- EFCore.BulkExtensions 8.1.1
- Microsoft.Data.SqlClient 5.2.2
