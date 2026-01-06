<#
.SYNOPSIS
    Generuje interaktywne wykresy HTML z wyników BenchmarkDotNet
    
.DESCRIPTION
    Ten skrypt tworzy wykresy HTML z Chart.js na podstawie plików CSV
    wygenerowanych przez BenchmarkDotNet. Nie wymaga Pythona ani R.
    Obs³uguje parametry benchmarków (np. RecordCount) tworz¹c osobne wykresy.
    
.EXAMPLE
    .\Generate-Charts.ps1
    
.NOTES
    Wykresy zostan¹ zapisane w folderze Charts/
#>

$ResultsDir = "BenchmarkDotNet.Artifacts\results"
$ChartsDir = "Charts"

# Utwórz folder na wykresy
if (-not (Test-Path $ChartsDir)) {
    New-Item -ItemType Directory -Path $ChartsDir | Out-Null
}

function Parse-TimeValue {
    param([string]$value)
    
    if ([string]::IsNullOrEmpty($value)) { return 0 }
    
    $value = $value.Trim()
    
    # Usuñ przecinki tysiêczne (np. "1,077.1")
    $value = $value -replace ",", ""
    
    # Wyodrêbnij liczbê z pocz¹tku stringa
    if ($value -match "^([0-9]+\.?[0-9]*)") {
        $num = [double]$matches[1]
        
        # SprawdŸ jednostkê
        if ($value -match "ns") {
            return $num / 1000
        }
        elseif ($value -match "ms") {
            return $num * 1000
        }
        else {
            # Domyœlnie ?s
            return $num
        }
    }
    
    return 0
}

function Parse-MemoryValue {
    param([string]$value)
    
    if ([string]::IsNullOrEmpty($value)) { return 0 }
    
    $value = $value.Trim() -replace ",", ""
    
    if ($value -match "^([0-9]+\.?[0-9]*)") {
        $num = [double]$matches[1]
        
        if ($value -match "MB") {
            return $num * 1024
        }
        elseif ($value -match "KB") {
            return $num
        }
        elseif ($value -match "B$") {
            return $num / 1024
        }
        else {
            return $num
        }
    }
    
    return 0
}

function Format-Number {
    param([double]$value)
    return [Math]::Round($value, 2).ToString([System.Globalization.CultureInfo]::InvariantCulture)
}

function Generate-HtmlChart {
    param(
        [string]$Title,
        [string]$Subtitle,
        [array]$Labels,
        [array]$TimeData,
        [array]$MemoryData,
        [string]$OutputPath
    )
    
    $labelsJson = ($Labels | ForEach-Object { "`"$_`"" }) -join ", "
    $timeJson = ($TimeData | ForEach-Object { Format-Number $_ }) -join ", "
    $memoryJson = ($MemoryData | ForEach-Object { Format-Number $_ }) -join ", "
    
    $colors = @(
        "rgba(54, 162, 235, 0.8)",
        "rgba(255, 99, 132, 0.8)",
        "rgba(75, 192, 192, 0.8)",
        "rgba(255, 206, 86, 0.8)",
        "rgba(153, 102, 255, 0.8)",
        "rgba(255, 159, 64, 0.8)",
        "rgba(46, 204, 113, 0.8)",
        "rgba(155, 89, 182, 0.8)",
        "rgba(52, 73, 94, 0.8)",
        "rgba(241, 196, 15, 0.8)",
        "rgba(230, 126, 34, 0.8)",
        "rgba(231, 76, 60, 0.8)"
    )
    
    $colorsJson = ($colors[0..([Math]::Min($Labels.Count - 1, $colors.Count - 1))] | ForEach-Object { "`"$_`"" }) -join ", "
    
    $subtitleHtml = if ($Subtitle) { "<p style='text-align:center;color:#666;margin-top:-20px;'>$Subtitle</p>" } else { "" }
    
    $html = @"
<!DOCTYPE html>
<html lang="pl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>$Title - Benchmark Results</title>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; background-color: #f5f5f5; }
        .container { max-width: 1400px; margin: 0 auto; }
        h1 { text-align: center; color: #333; margin-bottom: 10px; }
        .chart-container { background: white; border-radius: 10px; padding: 20px; margin-bottom: 30px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
        .chart-row { display: flex; gap: 20px; flex-wrap: wrap; }
        .chart-half { flex: 1; min-width: 400px; }
        h2 { color: #555; border-bottom: 2px solid #007bff; padding-bottom: 10px; }
        .stats { margin-top: 20px; padding: 15px; background: #f8f9fa; border-radius: 5px; }
        .stats h3 { margin-top: 0; color: #333; }
        table { width: 100%; border-collapse: collapse; margin-top: 10px; }
        th, td { padding: 10px; text-align: left; border-bottom: 1px solid #ddd; }
        th { background-color: #007bff; color: white; }
        tr:hover { background-color: #f1f1f1; }
        .fastest { background-color: #d4edda !important; font-weight: bold; }
        .slowest { background-color: #f8d7da !important; }
        .info-box { background: #e7f3ff; border-left: 4px solid #007bff; padding: 15px; margin-bottom: 20px; border-radius: 0 5px 5px 0; }
    </style>
</head>
<body>
    <div class="container">
        <h1>$Title</h1>
        $subtitleHtml
        
        <div class="info-box">
            <strong>Wygenerowano:</strong> $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")<br>
            <strong>Liczba metod:</strong> $($Labels.Count)
        </div>
        
        <div class="chart-row">
            <div class="chart-container chart-half">
                <h2>Czas wykonania (us)</h2>
                <canvas id="timeChart"></canvas>
            </div>
            <div class="chart-container chart-half">
                <h2>Alokacja pamieci (KB)</h2>
                <canvas id="memoryChart"></canvas>
            </div>
        </div>
        
        <div class="chart-container">
            <h2>Porownanie wzgledne</h2>
            <canvas id="comparisonChart"></canvas>
        </div>
        
        <div class="chart-container stats">
            <h3>Szczegolowe wyniki</h3>
            <table>
                <tr><th>Metoda</th><th>Sredni czas (us)</th><th>Pamiec (KB)</th><th>Wzgledem najszybszej</th></tr>
"@

    $minTime = ($TimeData | Measure-Object -Minimum).Minimum
    $maxTime = ($TimeData | Measure-Object -Maximum).Maximum
    
    for ($i = 0; $i -lt $Labels.Count; $i++) {
        $time = Format-Number $TimeData[$i]
        $memory = Format-Number $MemoryData[$i]
        $ratio = if ($minTime -gt 0) { Format-Number ($TimeData[$i] / $minTime) } else { "1" }
        
        $class = ""
        if ($TimeData[$i] -eq $minTime) { $class = "fastest" }
        elseif ($TimeData[$i] -eq $maxTime) { $class = "slowest" }
        
        $html += "<tr class=`"$class`"><td>$($Labels[$i])</td><td>$time us</td><td>$memory KB</td><td>${ratio}x</td></tr>`n"
    }

    $html += @"
            </table>
        </div>
    </div>
    
    <script>
        new Chart(document.getElementById('timeChart'), {
            type: 'bar',
            data: { labels: [$labelsJson], datasets: [{ label: 'Czas (us)', data: [$timeJson], backgroundColor: [$colorsJson], borderWidth: 1 }] },
            options: { indexAxis: 'y', responsive: true, plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true, title: { display: true, text: 'Czas wykonania (us)' } } } }
        });
        
        new Chart(document.getElementById('memoryChart'), {
            type: 'bar',
            data: { labels: [$labelsJson], datasets: [{ label: 'Pamiec (KB)', data: [$memoryJson], backgroundColor: [$colorsJson], borderWidth: 1 }] },
            options: { indexAxis: 'y', responsive: true, plugins: { legend: { display: false } }, scales: { x: { beginAtZero: true, title: { display: true, text: 'Alokacja pamieci (KB)' } } } }
        });
        
        new Chart(document.getElementById('comparisonChart'), {
            type: 'bar',
            data: { labels: [$labelsJson], datasets: [
                { label: 'Czas (us)', data: [$timeJson], backgroundColor: 'rgba(54, 162, 235, 0.8)', yAxisID: 'y' },
                { label: 'Pamiec (KB)', data: [$memoryJson], backgroundColor: 'rgba(255, 99, 132, 0.8)', yAxisID: 'y1' }
            ]},
            options: { responsive: true, plugins: { title: { display: true, text: 'Porownanie czasu i pamieci' } },
                scales: { y: { type: 'linear', position: 'left', title: { display: true, text: 'Czas (us)' } },
                          y1: { type: 'linear', position: 'right', title: { display: true, text: 'Pamiec (KB)' }, grid: { drawOnChartArea: false } } } }
        });
    </script>
</body>
</html>
"@

    $html | Out-File -FilePath $OutputPath -Encoding UTF8
}

# G³ówna logika
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Generator wykresow HTML z BenchmarkDotNet" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan

if (-not (Test-Path $ResultsDir)) {
    Write-Host "`nNie znaleziono folderu: $ResultsDir" -ForegroundColor Red
    exit
}

$csvFiles = Get-ChildItem -Path $ResultsDir -Filter "*-report.csv"

if ($csvFiles.Count -eq 0) {
    Write-Host "`nBrak plikow CSV w: $ResultsDir" -ForegroundColor Red
    exit
}

Write-Host "`nZnaleziono $($csvFiles.Count) plikow CSV" -ForegroundColor White

foreach ($csvFile in $csvFiles) {
    $benchmarkName = $csvFile.BaseName -replace "-report", ""
    $shortName = ($benchmarkName -split "\.")[-1]
    
    Write-Host "`nPrzetwarzanie: $shortName" -ForegroundColor Yellow
    
    try {
        $data = Import-Csv -Path $csvFile.FullName -Delimiter ";"
        
        if ($data.Count -eq 0) {
            Write-Host "  Brak danych w pliku" -ForegroundColor Yellow
            continue
        }
        
        # Pobierz nazwy kolumn
        $columnNames = $data[0].PSObject.Properties.Name
        
        # SprawdŸ czy jest kolumna RecordCount (lub inne parametry)
        $paramCol = $null
        foreach ($possibleParam in @("RecordCount", "Size", "Count", "N", "BatchSize")) {
            if ($columnNames -contains $possibleParam) {
                # SprawdŸ czy ma ró¿ne wartoœci
                $uniqueValues = $data | ForEach-Object { $_.$possibleParam } | Select-Object -Unique
                if ($uniqueValues.Count -gt 1) {
                    $paramCol = $possibleParam
                    break
                }
            }
        }
        
        if ($paramCol) {
            # S¹ parametry - generuj osobny wykres dla ka¿dej wartoœci
            $paramValues = $data | ForEach-Object { $_.$paramCol } | Select-Object -Unique | Sort-Object { [int]$_ }
            
            Write-Host "  Parametr: $paramCol = $($paramValues -join ', ')" -ForegroundColor Gray
            
            foreach ($paramValue in $paramValues) {
                $subset = $data | Where-Object { $_.$paramCol -eq $paramValue }
                
                $grouped = $subset | Sort-Object { Parse-TimeValue $_.Mean }
                
                $methods = @($grouped | ForEach-Object { $_.Method })
                $times = @($grouped | ForEach-Object { Parse-TimeValue $_.Mean })
                $memory = @($grouped | ForEach-Object { Parse-MemoryValue $_.Allocated })
                
                $outputPath = Join-Path $ChartsDir "$shortName`_$paramCol`_$paramValue.html"
                
                Generate-HtmlChart -Title $shortName -Subtitle "$paramCol = $paramValue" -Labels $methods -TimeData $times -MemoryData $memory -OutputPath $outputPath
                Write-Host "  Zapisano: $outputPath" -ForegroundColor Green
            }
        }
        else {
            # Brak parametrów - jeden wykres
            $grouped = $data | Sort-Object { Parse-TimeValue $_.Mean }
            
            $methods = @($grouped | ForEach-Object { $_.Method })
            $times = @($grouped | ForEach-Object { Parse-TimeValue $_.Mean })
            $memory = @($grouped | ForEach-Object { Parse-MemoryValue $_.Allocated })
            
            $outputPath = Join-Path $ChartsDir "$shortName.html"
            
            Generate-HtmlChart -Title $shortName -Subtitle "" -Labels $methods -TimeData $times -MemoryData $memory -OutputPath $outputPath
            Write-Host "  Zapisano: $outputPath" -ForegroundColor Green
        }
    }
    catch {
        Write-Host "  Blad: $_" -ForegroundColor Red
    }
}

Write-Host "`n========================================" -ForegroundColor Cyan
Write-Host "Wykresy zapisane w: $((Resolve-Path $ChartsDir).Path)" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan

Start-Process $ChartsDir
