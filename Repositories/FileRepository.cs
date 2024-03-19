using Newtonsoft.Json.Linq;
using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace securityApp.Repositories
{
    public class FileRepository : IFileRepository
    {
        private bool isMyFileReady = false;
        private const string filesLink = "https://www.virustotal.com/api/v3/files/";
        private const string folderName = "FilesToUpload";
        private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        
        private readonly VirusTotalSettings _totalSettings;
        private readonly Encoder _encoder;

        public FileRepository(VirusTotalSettings virusTotalSettings, Encoder encoder)
        {
            _totalSettings = virusTotalSettings;
            _encoder = encoder;
        }
        public async Task<RestResponse> GetFileResult(string fileSHA)
        {


            var options = new RestClientOptions($"{filesLink}{fileSHA}");
            Console.WriteLine($"{filesLink}{fileSHA}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            var response = await client.GetAsync(request);

            Console.WriteLine("{0}", response.Content);
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound )
            {
                Console.WriteLine(response.Content);
                await Task.Delay(6000);
                return await GetFileResult(fileSHA);
            }
            else
            {
                JObject result = JObject.Parse(response.Content);
                var lastAnalysisResults = result["data"]["attributes"]["last_analysis_results"];

                if (lastAnalysisResults.ToString() == "{}")
                {
                    Console.WriteLine(result["data"]["attributes"]["last_analysis_results"]);
                    await Task.Delay(5000);
                    return await GetFileResult(fileSHA);
                }
            }
            return response;
        }

        public async Task<RestResponse> UploadFile(IFormFile file)
        {

            var filePath = Path.Combine(folderPath, file.FileName);
            
            Console.WriteLine(filePath);
            using (var fileContentStream = new MemoryStream())
            {
                await file.CopyToAsync(fileContentStream);
                await System.IO.File.WriteAllBytesAsync(Path.Combine(folderPath, file.FileName),
                    fileContentStream.ToArray());
            }

            var options = new RestClientOptions(_totalSettings.FileLink);
            var client = new RestClient(options);
            var request = new RestRequest("");
            
            request.AlwaysMultipartFormData = true;
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            request.FormBoundary = "---011000010111000001101001";
            
            request.AddFile("file", filePath ,file.ContentType);
            var response = await client.PostAsync(request);
            Console.WriteLine("{0}", response.Content);
            return response;
        }


    }
}
