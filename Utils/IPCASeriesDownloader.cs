using System.IO.Compression;

namespace IPCAProcessor.Utils;

public static class IPCASeriesDownloader {
    private const string Url = "https://ftp.ibge.gov.br/Precos_Indices_de_Precos_ao_Consumidor/IPCA/Serie_Historica/ipca_SerieHist.zip";

    public static async void DownloadAndExtractAsync(string directoryToExtract) {
        var client = new HttpClient();
        var result = await client.GetAsync(Url);

        if(!result.IsSuccessStatusCode) {
            Console.WriteLine($"Request failed with code {result.StatusCode}");
        }

        ZipFile.ExtractToDirectory(result.Content.ReadAsStream(), directoryToExtract, true);
    }
}