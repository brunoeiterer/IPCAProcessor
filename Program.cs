using IPCAProcessor.Utils;
using IPCAProcessor.Models;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var extractedFileDirectory = Path.Combine(Environment.CurrentDirectory, "output");
Directory.CreateDirectory(extractedFileDirectory);

IPCASeriesDownloader.DownloadAndExtractAsync(extractedFileDirectory);

var ipcaSeries = IPCASeries.FromXls(Directory.GetFiles(extractedFileDirectory).Where(f => f.EndsWith("xls")).First());

var jsonData = ipcaSeries.ToJson(2010, 1, 2024, 12);
File.WriteAllText(Path.Combine(extractedFileDirectory, "IPCASeries.json"), jsonData);

ipcaSeries.Plot(IPCASeriesType.MonthlyVariation, 2010, 1, 2024, 12);
ipcaSeries.Plot(IPCASeriesType.ThreeMonthVariation, 2010, 1, 2024, 12);
ipcaSeries.Plot(IPCASeriesType.SixMonthVariation, 2010, 1, 2024, 12);
ipcaSeries.Plot(IPCASeriesType.TwelveMonthVariation, 2010, 1, 2024, 12);
ipcaSeries.Plot(IPCASeriesType.YearVariation, 2010, 1, 2024, 12);

ipcaSeries.ToCSV(2010, 1, 2024, 12);

var sqlData = ipcaSeries.ToSQL(2010, 1, 2024, 12);
File.WriteAllText(Path.Combine(extractedFileDirectory, "IPCASeries.sql"), sqlData);