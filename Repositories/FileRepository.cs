using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces;

namespace securityApp.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly VirusTotalSettings _totalSettings;
        public FileRepository(VirusTotalSettings virusTotalSettings)
        {
            _totalSettings = virusTotalSettings;
        }
        public Task<RestResponse> GetFileResult(string fileSHA)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> SendFile(IFormFile file)
        {
            var options = new RestClientOptions(_totalSettings.FileLink);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AlwaysMultipartFormData = true;
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            request.FormBoundary = "---011000010111000001101001";
            request.AddFile("file", file);
            var response = await client.PostAsync(request);

            Console.WriteLine("{0}", response.Content);
            return response;
        }
    }
}
