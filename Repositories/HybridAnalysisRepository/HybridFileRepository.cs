using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysisRepository;

namespace securityApp.Repositories.HybridAnalysisRepository
{
    public class HybridFileRepository : IHybridFileRepository
    {
        private readonly HybridAnalysisSettings _settings;
        private readonly FileHandler _fileHandler;
        public HybridFileRepository(HybridAnalysisSettings hybridAnalysisSettings, FileHandler fileHandler)
        {
            
            _fileHandler = fileHandler;
            _settings = hybridAnalysisSettings;
        }
        public async Task<RestResponse> GetFileReport(string sha256)
        {
            var options = new RestClientOptions($"{_settings.OverviewSha}{sha256}");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key",_settings.ApiKey);

            var response = await client.GetAsync(request);

            Console.WriteLine(response.Content);
            return response;


        }

        public async Task<RestResponse> SendFile(IFormFile file)
        {
            var filePath = Path.Combine(_fileHandler.folderPath, file.FileName);
            _fileHandler.CreateFile(file);

            var options = new RestClientOptions(_settings.QuickScanFile);
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AlwaysMultipartFormData = true;

            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key", _settings.ApiKey);
            request.AddHeader("Content-Type", "multipart/form-data");

            request.AddParameter("scan_type", "all");
            request.AddFile("file", filePath, file.ContentType);
            request.AddParameter("comment", "");
            request.AddParameter("submit_name", "");

            var response = await client.PostAsync(request);
            Console.WriteLine("{0}", response.Content);

            return response;

        }
    }
}
