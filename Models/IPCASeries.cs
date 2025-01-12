using System.Text.Json;
using ExcelDataReader;

namespace IPCAProcessor.Models;

public class IPCASeries : Dictionary<int, Dictionary<int, IPCAData>> {
    public static IPCASeries FromXls(string filePath) {
        using var reader = ExcelReaderFactory.CreateReader(File.Open(filePath, FileMode.Open));

        var ipcaSeries = new IPCASeries();

        var year = 0;
        while (reader.Read())
        {
            object[] values = new object[8];
            reader.GetValues(values);
            if(values.Any(v => v == null) || values[0] is string text && text == "ANO") {
                continue;
            }

            if(reader.GetFieldType(0) == typeof(double)) {
                year = (int)(double)values[0];
            }

            var yearData = ipcaSeries.GetValueOrDefault(year, []);
            yearData[MonthStringToInt((string)values[1])] = new IPCAData() {
                MonthlyVariation = (double)values[3],
                ThreeMonthVariation = (double)values[4],
                SixMonthVariation = (double)values[5],
                TwelveMonthVariation = (double)values[7],
                YearVariation = (double)values[6]
            };

            ipcaSeries[year] = yearData;
        }

        return ipcaSeries;
    }

    private readonly JsonSerializerOptions JsonSerializerOptions = new() {
        WriteIndented = true
    };

    public string ToJson() => JsonSerializer.Serialize(this, JsonSerializerOptions);

    private static int MonthStringToInt(string month) => month switch {
        "JAN" => 1,
        "FEV" => 2,
        "MAR" => 3,
        "ABR" => 4,
        "MAI" => 5,
        "JUN" => 6,
        "JUL" => 7,
        "AGO" => 8,
        "SET" => 9,
        "OUT" => 10,
        "NOV" => 11,
        "DEZ" => 12,
        _ => throw new InvalidOperationException("The string is not a valid month")
    };
}