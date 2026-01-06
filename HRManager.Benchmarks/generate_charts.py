"""
Skrypt do generowania wykresów z wyników BenchmarkDotNet
dla pracy magisterskiej.

Wymagane pakiety:
    pip install pandas matplotlib seaborn

U¿ycie:
    python generate_charts.py

Skrypt automatycznie znajdzie pliki CSV w BenchmarkDotNet.Artifacts/results/
i wygeneruje wykresy do folderu Charts/
"""

import os
import glob
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from pathlib import Path

# Konfiguracja stylu wykresów
plt.style.use('seaborn-v0_8-whitegrid')
sns.set_palette("husl")

# Œcie¿ki
RESULTS_DIR = Path("BenchmarkDotNet.Artifacts/results")
CHARTS_DIR = Path("Charts")

def setup_polish_labels():
    """Konfiguracja polskich etykiet dla wykresów"""
    return {
        'Mean': 'Œredni czas [?s]',
        'Median': 'Mediana [?s]',
        'Allocated': 'Alokacja pamiêci [KB]',
        'Method': 'Metoda',
        'RecordCount': 'Liczba rekordów'
    }

def load_benchmark_data(csv_path: str) -> pd.DataFrame:
    """Wczytuje dane z pliku CSV BenchmarkDotNet"""
    df = pd.read_csv(csv_path, sep=';')
    
    # Konwersja jednostek czasu do ?s
    if 'Mean' in df.columns:
        df['Mean_us'] = df['Mean'].apply(parse_time_to_microseconds)
    if 'Median' in df.columns:
        df['Median_us'] = df['Median'].apply(parse_time_to_microseconds)
    
    # Konwersja pamiêci do KB
    if 'Allocated' in df.columns:
        df['Allocated_KB'] = df['Allocated'].apply(parse_memory_to_kb)
    
    return df

def parse_time_to_microseconds(value) -> float:
    """Parsuje wartoœæ czasu do mikrosekund"""
    if pd.isna(value):
        return 0
    
    value = str(value).strip()
    
    if 'ns' in value:
        return float(value.replace('ns', '').strip().replace(',', '.')) / 1000
    elif '?s' in value or 'us' in value:
        return float(value.replace('?s', '').replace('us', '').strip().replace(',', '.'))
    elif 'ms' in value:
        return float(value.replace('ms', '').strip().replace(',', '.')) * 1000
    elif 's' in value:
        return float(value.replace('s', '').strip().replace(',', '.')) * 1_000_000
    
    try:
        return float(value.replace(',', '.'))
    except:
        return 0

def parse_memory_to_kb(value) -> float:
    """Parsuje wartoœæ pamiêci do KB"""
    if pd.isna(value):
        return 0
    
    value = str(value).strip()
    
    if 'B' not in value:
        try:
            return float(value.replace(',', '.')) / 1024
        except:
            return 0
    
    if 'KB' in value:
        return float(value.replace('KB', '').strip().replace(',', '.'))
    elif 'MB' in value:
        return float(value.replace('MB', '').strip().replace(',', '.')) * 1024
    elif 'GB' in value:
        return float(value.replace('GB', '').strip().replace(',', '.')) * 1024 * 1024
    else:  # Bytes
        return float(value.replace('B', '').strip().replace(',', '.')) / 1024

def create_bar_chart_time(df: pd.DataFrame, title: str, output_path: str):
    """Tworzy wykres s³upkowy czasu wykonania"""
    fig, ax = plt.subplots(figsize=(12, 6))
    
    methods = df['Method'].tolist()
    times = df['Mean_us'].tolist()
    
    colors = sns.color_palette("husl", len(methods))
    bars = ax.barh(methods, times, color=colors)
    
    ax.set_xlabel('Œredni czas wykonania [?s]', fontsize=12)
    ax.set_title(title, fontsize=14, fontweight='bold')
    
    # Dodaj wartoœci na s³upkach
    for bar, time in zip(bars, times):
        ax.text(bar.get_width() + max(times) * 0.01, bar.get_y() + bar.get_height()/2,
                f'{time:.2f} ?s', va='center', fontsize=9)
    
    plt.tight_layout()
    plt.savefig(output_path, dpi=300, bbox_inches='tight')
    plt.close()
    print(f"  ? Zapisano: {output_path}")

def create_bar_chart_memory(df: pd.DataFrame, title: str, output_path: str):
    """Tworzy wykres s³upkowy alokacji pamiêci"""
    if 'Allocated_KB' not in df.columns:
        return
    
    fig, ax = plt.subplots(figsize=(12, 6))
    
    methods = df['Method'].tolist()
    memory = df['Allocated_KB'].tolist()
    
    colors = sns.color_palette("husl", len(methods))
    bars = ax.barh(methods, memory, color=colors)
    
    ax.set_xlabel('Alokacja pamiêci [KB]', fontsize=12)
    ax.set_title(title, fontsize=14, fontweight='bold')
    
    for bar, mem in zip(bars, memory):
        ax.text(bar.get_width() + max(memory) * 0.01, bar.get_y() + bar.get_height()/2,
                f'{mem:.2f} KB', va='center', fontsize=9)
    
    plt.tight_layout()
    plt.savefig(output_path, dpi=300, bbox_inches='tight')
    plt.close()
    print(f"  ? Zapisano: {output_path}")

def create_comparison_chart(df: pd.DataFrame, title: str, output_path: str):
    """Tworzy wykres porównawczy czasu i pamiêci"""
    fig, axes = plt.subplots(1, 2, figsize=(16, 6))
    
    methods = df['Method'].tolist()
    times = df['Mean_us'].tolist()
    
    # Wykres czasu
    colors = sns.color_palette("husl", len(methods))
    axes[0].barh(methods, times, color=colors)
    axes[0].set_xlabel('Œredni czas [?s]', fontsize=11)
    axes[0].set_title('Czas wykonania', fontsize=12, fontweight='bold')
    
    # Wykres pamiêci
    if 'Allocated_KB' in df.columns:
        memory = df['Allocated_KB'].tolist()
        axes[1].barh(methods, memory, color=colors)
        axes[1].set_xlabel('Pamiêæ [KB]', fontsize=11)
        axes[1].set_title('Alokacja pamiêci', fontsize=12, fontweight='bold')
    
    fig.suptitle(title, fontsize=14, fontweight='bold', y=1.02)
    plt.tight_layout()
    plt.savefig(output_path, dpi=300, bbox_inches='tight')
    plt.close()
    print(f"  ? Zapisano: {output_path}")

def create_grouped_bar_chart(df: pd.DataFrame, group_col: str, title: str, output_path: str):
    """Tworzy wykres s³upkowy z grupowaniem (np. dla ró¿nych RecordCount)"""
    if group_col not in df.columns:
        return
    
    fig, ax = plt.subplots(figsize=(14, 8))
    
    groups = df[group_col].unique()
    methods = df['Method'].unique()
    
    x = range(len(methods))
    width = 0.8 / len(groups)
    
    for i, group in enumerate(groups):
        group_data = df[df[group_col] == group]
        times = [group_data[group_data['Method'] == m]['Mean_us'].values[0] 
                 if len(group_data[group_data['Method'] == m]) > 0 else 0 
                 for m in methods]
        
        offset = (i - len(groups)/2 + 0.5) * width
        ax.bar([xi + offset for xi in x], times, width, label=f'{group_col}={group}')
    
    ax.set_xlabel('Metoda', fontsize=12)
    ax.set_ylabel('Œredni czas [?s]', fontsize=12)
    ax.set_title(title, fontsize=14, fontweight='bold')
    ax.set_xticks(x)
    ax.set_xticklabels(methods, rotation=45, ha='right')
    ax.legend(title=group_col)
    
    plt.tight_layout()
    plt.savefig(output_path, dpi=300, bbox_inches='tight')
    plt.close()
    print(f"  ? Zapisano: {output_path}")

def create_speedup_chart(df: pd.DataFrame, baseline_method: str, title: str, output_path: str):
    """Tworzy wykres przyspieszenia wzglêdem metody bazowej"""
    if baseline_method not in df['Method'].values:
        return
    
    baseline_time = df[df['Method'] == baseline_method]['Mean_us'].values[0]
    
    fig, ax = plt.subplots(figsize=(12, 6))
    
    methods = df['Method'].tolist()
    speedups = [baseline_time / t if t > 0 else 0 for t in df['Mean_us'].tolist()]
    
    colors = ['green' if s > 1 else 'red' for s in speedups]
    bars = ax.barh(methods, speedups, color=colors)
    
    ax.axvline(x=1, color='black', linestyle='--', linewidth=1, label='Baseline')
    ax.set_xlabel(f'Przyspieszenie wzglêdem {baseline_method}', fontsize=12)
    ax.set_title(title, fontsize=14, fontweight='bold')
    
    for bar, speedup in zip(bars, speedups):
        ax.text(bar.get_width() + 0.05, bar.get_y() + bar.get_height()/2,
                f'{speedup:.2f}x', va='center', fontsize=9)
    
    plt.tight_layout()
    plt.savefig(output_path, dpi=300, bbox_inches='tight')
    plt.close()
    print(f"  ? Zapisano: {output_path}")

def process_benchmark_file(csv_path: str):
    """Przetwarza pojedynczy plik CSV i generuje wykresy"""
    filename = Path(csv_path).stem.replace('-report', '')
    benchmark_name = filename.split('.')[-1]
    
    print(f"\n?? Przetwarzanie: {benchmark_name}")
    
    try:
        df = load_benchmark_data(csv_path)
        
        if df.empty or 'Method' not in df.columns:
            print(f"  ? Brak danych w pliku")
            return
        
        # Utwórz folder dla wykresów
        chart_dir = CHARTS_DIR / benchmark_name
        chart_dir.mkdir(parents=True, exist_ok=True)
        
        # Wykres czasu wykonania
        create_bar_chart_time(
            df, 
            f'{benchmark_name}\nCzas wykonania',
            chart_dir / 'execution_time.png'
        )
        
        # Wykres alokacji pamiêci
        create_bar_chart_memory(
            df,
            f'{benchmark_name}\nAlokacja pamiêci',
            chart_dir / 'memory_allocation.png'
        )
        
        # Wykres porównawczy
        create_comparison_chart(
            df,
            f'{benchmark_name}\nPorównanie wydajnoœci',
            chart_dir / 'comparison.png'
        )
        
        # Wykres z grupowaniem (jeœli s¹ parametry)
        for param_col in ['RecordCount', 'Size', 'Count']:
            if param_col in df.columns:
                create_grouped_bar_chart(
                    df,
                    param_col,
                    f'{benchmark_name}\nPorównanie dla ró¿nych wartoœci {param_col}',
                    chart_dir / f'grouped_by_{param_col.lower()}.png'
                )
        
        # Wykres przyspieszenia
        baseline_methods = df[df['Method'].str.contains('Baseline|EFCore_Find|EFCore_AddRange', case=False, na=False)]
        if not baseline_methods.empty:
            baseline = baseline_methods['Method'].values[0]
            create_speedup_chart(
                df,
                baseline,
                f'{benchmark_name}\nPrzyspieszenie wzglêdem {baseline}',
                chart_dir / 'speedup.png'
            )
        
    except Exception as e:
        print(f"  ? B³¹d: {e}")

def main():
    """G³ówna funkcja"""
    print("=" * 60)
    print("?? Generator wykresów BenchmarkDotNet")
    print("   dla pracy magisterskiej")
    print("=" * 60)
    
    # SprawdŸ czy istnieje folder z wynikami
    if not RESULTS_DIR.exists():
        print(f"\n? Nie znaleziono folderu: {RESULTS_DIR}")
        print("   Najpierw uruchom benchmarki!")
        return
    
    # ZnajdŸ wszystkie pliki CSV
    csv_files = list(RESULTS_DIR.glob("*-report.csv"))
    
    if not csv_files:
        print(f"\n? Brak plików CSV w: {RESULTS_DIR}")
        return
    
    print(f"\n?? Znaleziono {len(csv_files)} plików CSV")
    
    # Utwórz folder na wykresy
    CHARTS_DIR.mkdir(exist_ok=True)
    
    # Przetwórz ka¿dy plik
    for csv_file in csv_files:
        process_benchmark_file(str(csv_file))
    
    print("\n" + "=" * 60)
    print(f"? Wykresy zapisane w: {CHARTS_DIR.absolute()}")
    print("=" * 60)

if __name__ == "__main__":
    main()
