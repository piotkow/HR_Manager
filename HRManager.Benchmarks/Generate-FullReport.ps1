<#
.SYNOPSIS
    Generuje zbiorczy raport HTML ze wszystkimi benchmarkami dla pracy magisterskiej
    
.DESCRIPTION
    Ten skrypt tworzy jeden plik HTML zawieraj¹cy wszystkie scenariusze testowe
    z wykresami Chart.js. Idealny do prezentacji wyników w pracy dyplomowej.
    
.EXAMPLE
    .\Generate-FullReport.ps1
    
.NOTES
    Raport zostanie zapisany jako Charts\FullBenchmarkReport.html
#>

$ResultsDir = "BenchmarkDotNet.Artifacts\results"
$ChartsDir = "Charts"
$OutputFile = Join-Path $ChartsDir "FullBenchmarkReport.html"

# Utwórz folder na wykresy
if (-not (Test-Path $ChartsDir)) {
    New-Item -ItemType Directory -Path $ChartsDir | Out-Null
}

function Parse-TimeValue {
    param([string]$value)
    if ([string]::IsNullOrEmpty($value)) { return 0 }
    $value = $value.Trim() -replace ",", ""
    if ($value -match "^([0-9]+\.?[0-9]*)") {
        $num = [double]$matches[1]
        if ($value -match "ns") { return $num / 1000 }
        elseif ($value -match "ms") { return $num * 1000 }
        else { return $num }
    }
    return 0
}

function Parse-MemoryValue {
    param([string]$value)
    if ([string]::IsNullOrEmpty($value)) { return 0 }
    $value = $value.Trim() -replace ",", ""
    if ($value -match "^([0-9]+\.?[0-9]*)") {
        $num = [double]$matches[1]
        if ($value -match "MB") { return $num * 1024 }
        elseif ($value -match "KB") { return $num }
        elseif ($value -match "B$") { return $num / 1024 }
        else { return $num }
    }
    return 0
}

function Format-Number {
    param([double]$value)
    return [Math]::Round($value, 2).ToString([System.Globalization.CultureInfo]::InvariantCulture)
}

# Scenariusze z opisami
$scenarios = @{
    "TrackingVsNoTrackingBenchmarks" = @{
        Number = 1
        Title = "Scenariusz 1: Tracking vs NoTracking"
        Description = "Badanie narzutu wydajnosciowego ChangeTrackera przy operacjach tylko do odczytu."
        Group = "A"
    }
    "LoadingStrategiesBenchmarks" = @{
        Number = 2
        Title = "Scenariusz 2: Strategie ladowania danych"
        Description = "Porownanie Eager Loading (Include), Explicit Loading oraz Select Loading (projekcja do DTO)."
        Group = "A"
    }
    "ProjectionBenchmarks" = @{
        Number = 3
        Title = "Scenariusz 3: Projekcja danych"
        Description = "Porownanie pobierania pelnych encji vs projekcja do DTO."
        Group = "A"
    }
    "SplitQueryBenchmarks" = @{
        Number = 4
        Title = "Scenariusz 4: Split Queries"
        Description = "Porownanie Single Query vs Split Query - problem iloczynu kartezjanskiego."
        Group = "A"
    }
    "BulkOperationsBenchmarks" = @{
        Number = 5
        Title = "Scenariusz 5: Operacje masowe"
        Description = "Porownanie AddRange, ExecuteUpdate/Delete oraz BulkExtensions."
        Group = "A"
    }
    "SimpleQueryComparisonBenchmarks" = @{
        Number = 6
        Title = "Scenariusz 6: Prosty odczyt (GetById)"
        Description = "Porownanie EF Core vs Dapper vs ADO.NET dla prostego zapytania."
        Group = "B"
    }
    "ComplexQueryComparisonBenchmarks" = @{
        Number = 7
        Title = "Scenariusz 7: Raportowanie (Complex Query)"
        Description = "Skomplikowane zapytanie agregujace z wieloma zlaczeniami."
        Group = "B"
    }
    "HighPerformanceInsertBenchmarks" = @{
        Number = 8
        Title = "Scenariusz 8: Wysokowydajny zapis"
        Description = "Masowe wstawianie danych - EF Core vs Dapper vs ADO.NET vs SqlBulkCopy."
        Group = "B"
    }
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Generator zbiorczego raportu benchmarkow" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if (-not (Test-Path $ResultsDir)) {
    Write-Host "`nNie znaleziono folderu: $ResultsDir" -ForegroundColor Red
    exit
}

$csvFiles = Get-ChildItem -Path $ResultsDir -Filter "*-report.csv"

# Rozpocznij HTML
$html = @"
<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Analiza wydajnosci Entity Framework Core - Wyniki benchmarkow</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        * { box-sizing: border-box; }
        body { 
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
            margin: 0; 
            padding: 20px;
            background-color: #f0f2f5; 
            color: #333;
        }
        .container { max-width: 1400px; margin: 0 auto; }
        
        /* Header */
        .header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 40px;
            border-radius: 15px;
            margin-bottom: 30px;
            text-align: center;
        }
        .header h1 { margin: 0 0 10px 0; font-size: 2.5em; }
        .header p { margin: 0; opacity: 0.9; font-size: 1.2em; }
        
        /* Navigation */
        .nav {
            background: white;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 30px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .nav h3 { margin-top: 0; color: #667eea; }
        .nav-group { margin-bottom: 15px; }
        .nav-group-title { font-weight: bold; color: #555; margin-bottom: 8px; }
        .nav a {
            display: inline-block;
            padding: 8px 15px;
            margin: 3px;
            background: #f0f2f5;
            color: #333;
            text-decoration: none;
            border-radius: 5px;
            transition: all 0.3s;
        }
        .nav a:hover { background: #667eea; color: white; }
        
        /* Scenario sections */
        .scenario {
            background: white;
            border-radius: 15px;
            padding: 30px;
            margin-bottom: 30px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.1);
        }
        .scenario-header {
            border-bottom: 3px solid #667eea;
            padding-bottom: 15px;
            margin-bottom: 25px;
        }
        .scenario-header h2 { 
            margin: 0; 
            color: #333;
            display: flex;
            align-items: center;
            gap: 10px;
        }
        .scenario-number {
            background: #667eea;
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 0.8em;
        }
        .scenario-description {
            color: #666;
            font-style: italic;
            margin-top: 10px;
        }
        
        /* Parameter tabs */
        .param-tabs {
            display: flex;
            gap: 5px;
            margin-bottom: 20px;
            flex-wrap: wrap;
        }
        .param-tab {
            padding: 10px 20px;
            background: #e9ecef;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.3s;
        }
        .param-tab:hover { background: #dee2e6; }
        .param-tab.active { background: #667eea; color: white; }
        
        .param-content { display: none; }
        .param-content.active { display: block; }
        
        /* Charts */
        .charts-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            margin-bottom: 20px;
        }
        @media (max-width: 900px) {
            .charts-row { grid-template-columns: 1fr; }
        }
        .chart-box {
            background: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
        }
        .chart-box h4 {
            margin: 0 0 15px 0;
            color: #555;
            font-size: 1em;
        }
        
        /* Tables */
        table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            font-size: 14px;
        }
        th, td {
            padding: 12px 15px;
            text-align: left;
            border-bottom: 1px solid #e9ecef;
        }
        th {
            background: #667eea;
            color: white;
            font-weight: 600;
        }
        tr:hover { background: #f8f9fa; }
        .fastest { background: #d4edda !important; font-weight: bold; }
        .slowest { background: #f8d7da !important; }
        
        /* Summary */
        .summary-box {
            background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
            border-left: 4px solid #667eea;
            padding: 20px;
            border-radius: 0 10px 10px 0;
            margin-top: 20px;
        }
        .summary-box h4 { margin-top: 0; color: #667eea; }
        
        /* Footer */
        .footer {
            text-align: center;
            padding: 30px;
            color: #666;
            font-size: 0.9em;
        }
        
        /* Group badge */
        .group-badge {
            display: inline-block;
            padding: 3px 10px;
            border-radius: 10px;
            font-size: 0.7em;
            margin-left: 10px;
        }
        .group-a { background: #28a745; color: white; }
        .group-b { background: #dc3545; color: white; }
    </style>
</head>
<body>
    <div class="container">
        <div class="header">
            <h1>Analiza wydajnosci Entity Framework Core</h1>
            <p>Wyniki badan benchmarkowych - $(Get-Date -Format "yyyy-MM-dd HH:mm")</p>
        </div>
        
        <div class="nav">
            <h3>Spis tresci</h3>
            <div class="nav-group">
                <div class="nav-group-title">Grupa A: Optymalizacja wewnetrzna EF Core</div>
                <a href="#scenario-1">1. Tracking vs NoTracking</a>
                <a href="#scenario-2">2. Strategie ladowania</a>
                <a href="#scenario-3">3. Projekcja danych</a>
                <a href="#scenario-4">4. Split Queries</a>
                <a href="#scenario-5">5. Operacje masowe</a>
            </div>
            <div class="nav-group">
                <div class="nav-group-title">Grupa B: EF Core vs Dapper vs ADO.NET</div>
                <a href="#scenario-6">6. Prosty odczyt</a>
                <a href="#scenario-7">7. Raportowanie</a>
                <a href="#scenario-8">8. Wysokowydajny zapis</a>
            </div>
        </div>
"@

$chartId = 0

foreach ($csvFile in $csvFiles | Sort-Object { 
    $name = $_.BaseName -replace "-report", "" -replace ".*\.", ""
    if ($scenarios.ContainsKey($name)) { $scenarios[$name].Number } else { 99 }
}) {
    $benchmarkName = $csvFile.BaseName -replace "-report", ""
    $shortName = ($benchmarkName -split "\.")[-1]
    
    if (-not $scenarios.ContainsKey($shortName)) { continue }
    
    $scenario = $scenarios[$shortName]
    Write-Host "Przetwarzanie: $($scenario.Title)" -ForegroundColor Yellow
    
    try {
        $data = Import-Csv -Path $csvFile.FullName -Delimiter ";"
        if ($data.Count -eq 0) { continue }
        
        $columnNames = $data[0].PSObject.Properties.Name
        $groupBadge = if ($scenario.Group -eq "A") { "<span class='group-badge group-a'>Grupa A</span>" } else { "<span class='group-badge group-b'>Grupa B</span>" }
        
        $html += @"
        
        <div class="scenario" id="scenario-$($scenario.Number)">
            <div class="scenario-header">
                <h2>
                    <span class="scenario-number">$($scenario.Number)</span>
                    $($scenario.Title -replace "Scenariusz \d+: ", "")
                    $groupBadge
                </h2>
                <p class="scenario-description">$($scenario.Description)</p>
            </div>
"@
        
        # SprawdŸ parametry
        $paramCol = $null
        foreach ($possibleParam in @("RecordCount", "Size", "Count", "N", "BatchSize")) {
            if ($columnNames -contains $possibleParam) {
                $uniqueValues = $data | ForEach-Object { $_.$possibleParam } | Select-Object -Unique
                if ($uniqueValues.Count -gt 1) {
                    $paramCol = $possibleParam
                    break
                }
            }
        }
        
        if ($paramCol) {
            $paramValues = $data | ForEach-Object { $_.$paramCol } | Select-Object -Unique | Sort-Object { [int]$_ }
            
            # Tabs
            $html += "<div class='param-tabs'>`n"
            $first = $true
            foreach ($pv in $paramValues) {
                $activeClass = if ($first) { "active" } else { "" }
                $html += "<button class='param-tab $activeClass' onclick='showTab(this, `"tab-$chartId-$pv`")'>$paramCol = $pv</button>`n"
                $first = $false
            }
            $html += "</div>`n"
            
            # Content for each param
            $first = $true
            foreach ($paramValue in $paramValues) {
                $activeClass = if ($first) { "active" } else { "" }
                $subset = $data | Where-Object { $_.$paramCol -eq $paramValue } | Sort-Object { Parse-TimeValue $_.Mean }
                
                $methods = @($subset | ForEach-Object { $_.Method })
                $times = @($subset | ForEach-Object { Parse-TimeValue $_.Mean })
                $memory = @($subset | ForEach-Object { Parse-MemoryValue $_.Allocated })
                
                $labelsJson = ($methods | ForEach-Object { "`"$_`"" }) -join ", "
                $timeJson = ($times | ForEach-Object { Format-Number $_ }) -join ", "
                $memoryJson = ($memory | ForEach-Object { Format-Number $_ }) -join ", "
                
                $minTime = ($times | Measure-Object -Minimum).Minimum
                $maxTime = ($times | Measure-Object -Maximum).Maximum
                
                $html += @"
            <div class="param-content $activeClass" id="tab-$chartId-$paramValue">
                <div class="charts-row">
                    <div class="chart-box">
                        <h4>Czas wykonania (us)</h4>
                        <canvas id="timeChart$chartId-$paramValue"></canvas>
                    </div>
                    <div class="chart-box">
                        <h4>Alokacja pamieci (KB)</h4>
                        <canvas id="memChart$chartId-$paramValue"></canvas>
                    </div>
                </div>
                <table>
                    <tr><th>Metoda</th><th>Sredni czas</th><th>Pamiec</th><th>Wzglednie</th></tr>
"@
                for ($i = 0; $i -lt $methods.Count; $i++) {
                    $class = ""
                    if ($times[$i] -eq $minTime) { $class = "fastest" }
                    elseif ($times[$i] -eq $maxTime) { $class = "slowest" }
                    $ratio = if ($minTime -gt 0) { Format-Number ($times[$i] / $minTime) } else { "1" }
                    $html += "<tr class='$class'><td>$($methods[$i])</td><td>$(Format-Number $times[$i]) us</td><td>$(Format-Number $memory[$i]) KB</td><td>${ratio}x</td></tr>`n"
                }
                
                $html += @"
                </table>
                <script>
                    new Chart(document.getElementById('timeChart$chartId-$paramValue'), {
                        type: 'bar',
                        data: { labels: [$labelsJson], datasets: [{ data: [$timeJson], backgroundColor: 'rgba(102, 126, 234, 0.8)' }] },
                        options: { indexAxis: 'y', plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true } } }
                    });
                    new Chart(document.getElementById('memChart$chartId-$paramValue'), {
                        type: 'bar',
                        data: { labels: [$labelsJson], datasets: [{ data: [$memoryJson], backgroundColor: 'rgba(118, 75, 162, 0.8)' }] },
                        options: { indexAxis: 'y', plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true } } }
                    });
                </script>
            </div>
"@
                $first = $false
            }
        }
        else {
            # No parameters - single view
            $sorted = $data | Sort-Object { Parse-TimeValue $_.Mean }
            $methods = @($sorted | ForEach-Object { $_.Method })
            $times = @($sorted | ForEach-Object { Parse-TimeValue $_.Mean })
            $memory = @($sorted | ForEach-Object { Parse-MemoryValue $_.Allocated })
            
            $labelsJson = ($methods | ForEach-Object { "`"$_`"" }) -join ", "
            $timeJson = ($times | ForEach-Object { Format-Number $_ }) -join ", "
            $memoryJson = ($memory | ForEach-Object { Format-Number $_ }) -join ", "
            
            $minTime = ($times | Measure-Object -Minimum).Minimum
            $maxTime = ($times | Measure-Object -Maximum).Maximum
            
            $html += @"
            <div class="charts-row">
                <div class="chart-box">
                    <h4>Czas wykonania (us)</h4>
                    <canvas id="timeChart$chartId"></canvas>
                </div>
                <div class="chart-box">
                    <h4>Alokacja pamieci (KB)</h4>
                    <canvas id="memChart$chartId"></canvas>
                </div>
            </div>
            <table>
                <tr><th>Metoda</th><th>Sredni czas</th><th>Pamiec</th><th>Wzglednie</th></tr>
"@
            for ($i = 0; $i -lt $methods.Count; $i++) {
                $class = ""
                if ($times[$i] -eq $minTime) { $class = "fastest" }
                elseif ($times[$i] -eq $maxTime) { $class = "slowest" }
                $ratio = if ($minTime -gt 0) { Format-Number ($times[$i] / $minTime) } else { "1" }
                $html += "<tr class='$class'><td>$($methods[$i])</td><td>$(Format-Number $times[$i]) us</td><td>$(Format-Number $memory[$i]) KB</td><td>${ratio}x</td></tr>`n"
            }
            
            $html += @"
            </table>
            <script>
                new Chart(document.getElementById('timeChart$chartId'), {
                    type: 'bar',
                    data: { labels: [$labelsJson], datasets: [{ data: [$timeJson], backgroundColor: 'rgba(102, 126, 234, 0.8)' }] },
                    options: { indexAxis: 'y', plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true } } }
                });
                new Chart(document.getElementById('memChart$chartId'), {
                    type: 'bar',
                    data: { labels: [$labelsJson], datasets: [{ data: [$memoryJson], backgroundColor: 'rgba(118, 75, 162, 0.8)' }] },
                    options: { indexAxis: 'y', plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true } } }
                });
            </script>
"@
        }
        
        $html += "</div>`n"
        $chartId++
        
    }
    catch {
        Write-Host "  Blad: $_" -ForegroundColor Red
    }
}

# Footer and scripts
$html += @"
        
        <div class="footer">
            <p>Wygenerowano automatycznie przez BenchmarkDotNet + PowerShell</p>
            <p>Srodowisko: .NET 8.0 | Windows 11 | $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")</p>
        </div>
    </div>
    
    <script>
        function showTab(btn, tabId) {
            // Find parent scenario
            var scenario = btn.closest('.scenario');
            
            // Deactivate all tabs and content in this scenario
            scenario.querySelectorAll('.param-tab').forEach(t => t.classList.remove('active'));
            scenario.querySelectorAll('.param-content').forEach(c => c.classList.remove('active'));
            
            // Activate clicked tab and its content
            btn.classList.add('active');
            document.getElementById(tabId).classList.add('active');
        }
    </script>
</body>
</html>
"@

$html | Out-File -FilePath $OutputFile -Encoding UTF8

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Raport zapisany: $OutputFile" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan

Start-Process $OutputFile
