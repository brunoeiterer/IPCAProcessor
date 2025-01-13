using System.Text.Json;
using ExcelDataReader;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace IPCAProcessor.Models;

public enum IPCASeriesType
{
    MonthlyVariation,
    ThreeMonthVariation,
    SixMonthVariation,
    TwelveMonthVariation,
    YearVariation
};

public class IPCASeries : Dictionary<int, Dictionary<int, IPCAData>>
{
    private readonly string PlotOutputPath = Path.Combine(Environment.CurrentDirectory, "output");

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

    public string ToJson(int fromYear = 0, int fromMonth = 1, int toYear = 9999, int toMonth = 12) =>
        JsonSerializer.Serialize(FilterData(fromYear, fromMonth, toYear, toMonth), JsonSerializerOptions);

    public void Plot(IPCASeriesType type, int fromYear = 0, int fromMonth = 1, int toYear = 9999, int toMonth = 12)
    {

        var plotModel = new PlotModel
        {
            Title = type switch
            {
                IPCASeriesType.MonthlyVariation => "IPCA Monthly Variation",
                IPCASeriesType.ThreeMonthVariation => "IPCA Three Month Variation",
                IPCASeriesType.SixMonthVariation => "IPCA Six Month Variation",
                IPCASeriesType.TwelveMonthVariation => "IPCA Twelve Month Variation",
                IPCASeriesType.YearVariation => "IPCA Year Variation",
                _ => throw new InvalidOperationException("The specified IPCASeriesType is not valid")
            }
        };

        var lineSeries = new LineSeries
        {
            MarkerType = MarkerType.Circle
        };

        var filteredIPCAData = FilterData(fromYear, fromMonth, toYear, toMonth);
        foreach (var yearKvp in filteredIPCAData)
        {
            foreach (var monthKvp in yearKvp.Value)
            {
                var date = new DateTime(yearKvp.Key, monthKvp.Key, 1);
                lineSeries.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), type switch
                {
                    IPCASeriesType.MonthlyVariation => monthKvp.Value.MonthlyVariation,
                    IPCASeriesType.ThreeMonthVariation => monthKvp.Value.ThreeMonthVariation,
                    IPCASeriesType.SixMonthVariation => monthKvp.Value.SixMonthVariation,
                    IPCASeriesType.TwelveMonthVariation => monthKvp.Value.TwelveMonthVariation,
                    IPCASeriesType.YearVariation => monthKvp.Value.YearVariation,
                    _ => throw new InvalidOperationException("The specified IPCASeriesType is not valid")
                }));
            }
        }

        plotModel.Series.Add(lineSeries);

        plotModel.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "yyyy/MM",
        });

        using var stream = File.Create(Path.Combine(PlotOutputPath, type switch
        {
            IPCASeriesType.MonthlyVariation => "IPCAMonthlyVariation.png",
            IPCASeriesType.ThreeMonthVariation => "IPCAThreeMonthVariation.png",
            IPCASeriesType.SixMonthVariation => "IPCASixMonthVariation.png",
            IPCASeriesType.TwelveMonthVariation => "IPCATwelveMonthVariation.png",
            IPCASeriesType.YearVariation => "IPCAYearVariation.png",
            _ => throw new InvalidOperationException("The specified IPCASeriesType is not valid")
        }));
        var pngExporter = new PngExporter { Width = 1024, Height = 768 };
        pngExporter.Export(plotModel, stream);
    }

    private Dictionary<int, Dictionary<int, IPCAData>> FilterData(int fromYear, int fromMonth, int toYear, int toMonth) =>
        this.Where(kvp => kvp.Key >= fromYear && kvp.Key <= toYear)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value
                .Where(kvp => kvp.Key >= fromMonth && kvp.Key <= toMonth)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

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