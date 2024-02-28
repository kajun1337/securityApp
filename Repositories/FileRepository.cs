using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces;
using System.Diagnostics;

namespace securityApp.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly VirusTotalSettings _totalSettings;
        private readonly Encoder _encoder;
        public FileRepository(VirusTotalSettings virusTotalSettings, Encoder encoder)
        {
            _totalSettings = virusTotalSettings;
            _encoder = encoder;
        }
        public async Task<RestResponse> GetFileResult(string fileSHA)
        {
            var options = new RestClientOptions($"https://www.virustotal.com/api/v3/files/{fileSHA}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            var response = await client.GetAsync(request);

            Console.WriteLine("{0}", response.Content);
            return response;
        }

        public async Task<RestResponse> UploadFile(IFormFile file)
        {
            Console.WriteLine(_encoder.EncodeFileToSHA265(file));
            var options = new RestClientOptions(_totalSettings.FileLink);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AlwaysMultipartFormData = true;
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            request.FormBoundary = "---011000010111000001101001";
            Console.WriteLine(file.FileName);
            Console.WriteLine(Path.GetFullPath(file.FileName));
            request.AddFile("file", file.FileName);
            var response = await client.PostAsync(request);

            Console.WriteLine("{0}", response.Content);
            return response;
        }
    }
}
