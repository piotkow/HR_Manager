# ============================================================
# Skrypt R do generowania wykresów z BenchmarkDotNet
# ============================================================
# 
# Ten skrypt jest automatycznie generowany przez BenchmarkDotNet
# gdy u¿ywasz RPlotExporter.
#
# INSTALACJA R:
# 1. Pobierz R z: https://www.r-project.org/
# 2. Zainstaluj wymagane pakiety:
#    install.packages(c("ggplot2", "dplyr", "tidyr", "gridExtra"))
#
# U¯YCIE:
# 1. Uruchom benchmarki z RPlotExporter
# 2. PrzejdŸ do folderu BenchmarkDotNet.Artifacts/results
# 3. Uruchom: Rscript BuildPlots.R
#
# BenchmarkDotNet automatycznie generuje plik BuildPlots.R
# ============================================================

# SprawdŸ czy jesteœmy w odpowiednim folderze
if (!file.exists("BuildPlots.R")) {
    cat("Ten skrypt to tylko instrukcja.\n")
    cat("BenchmarkDotNet automatycznie generuje BuildPlots.R w folderze results.\n")
    cat("\nAby wygenerowaæ wykresy:\n")
    cat("1. Uruchom benchmarki\n")
    cat("2. cd BenchmarkDotNet.Artifacts/results\n")
    cat("3. Rscript BuildPlots.R\n")
}

# Przyk³adowy kod do tworzenia w³asnych wykresów z danych CSV
# ------------------------------------------------------------

# library(ggplot2)
# library(dplyr)
# 
# # Wczytaj dane
# data <- read.csv("HRManager.Benchmarks.Benchmarks.SimpleQueryComparisonBenchmarks-report.csv", 
#                  sep = ";", stringsAsFactors = FALSE)
# 
# # Wykres czasu wykonania
# ggplot(data, aes(x = reorder(Method, Mean), y = Mean, fill = Method)) +
#     geom_bar(stat = "identity") +
#     coord_flip() +
#     labs(title = "Porównanie czasu wykonania",
#          x = "Metoda",
#          y = "Œredni czas [?s]") +
#     theme_minimal() +
#     theme(legend.position = "none")
# 
# ggsave("execution_time.png", width = 12, height = 6, dpi = 300)
