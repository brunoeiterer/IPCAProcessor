using System.Globalization;

namespace IPCAProcessor.Models;

public class IPCAData
{
    public double MonthlyVariation { get; set; }
    public double ThreeMonthVariation { get; set; }
    public double SixMonthVariation { get; set; }
    public double TwelveMonthVariation { get; set; }
    public double YearVariation { get; set; }

    public override string ToString() => string.Create(CultureInfo.InvariantCulture,
        $"{MonthlyVariation}, {ThreeMonthVariation}, {SixMonthVariation}, {TwelveMonthVariation}, {YearVariation}");
}