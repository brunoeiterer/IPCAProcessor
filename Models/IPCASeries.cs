using System.Text.Json;
using ExcelDataReader;

namespace IPCAProcessor.Models;

public class IPCASeries : Dictionary<int, Dictionary<int, IPCAData>>
{
    public static IPCASeries FromXls(string filePath)
    {
        using var reader = ExcelReaderFactory.CreateReader(File.Open(filePath, FileMode.Open));

        var ipcaSeries = new IPCASeries();

        var year = 0;
        while (reader.Read())
        {
            object[] values = new object[8];
            reader.GetValues(values);
            if (values[1] == null || values[0] is string text && text == "ANO")
            {
                continue;
            }

            if (reader.GetFieldType(0) == typeof(double))
            {
                year = (int)(double)values[0];
            }

            var yearData = ipcaSeries.GetValueOrDefault(year, []);
            yearData[MonthStringToInt((string)values[1])] = new IPCAData()
            {
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

    private readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true
    };

    public string ToJson(int fromYear = 0, int fromMonth = 1, int toYear = 9999, int toMonth = 12)
    {
        var filteredIPCASeries = this.Where(kvp => kvp.Key >= fromYear && kvp.Key <= toYear)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value
                .Where(kvp => kvp.Key >= fromMonth && kvp.Key <= toMonth)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
        );

        return JsonSerializer.Serialize(filteredIPCASeries, JsonSerializerOptions);
    }

    private static int MonthStringToInt(string month) => month switch
    {
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