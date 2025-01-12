using IPCAProcessor.Utils;
using IPCAProcessor.Models;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var extractedFileDirectory = Path.Combine(Environment.CurrentDirectory, "ipcaFiles");
IPCASeriesDownloader.DownloadAndExtractAsync(extractedFileDirectory);

var ipcaSeries = IPCASeries.FromXls(Directory.GetFiles(extractedFileDirectory).First());
var jsonData = ipcaSeries.ToJson();

Console.WriteLine(jsonData);
File.WriteAllText("IPCASeries.json", jsonData);

