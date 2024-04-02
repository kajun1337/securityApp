using RestSharp;
using RestSharp.Authenticators;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysesRepository;
using Newtonsoft.Json;

namespace securityApp.Repositories.HybridAnalysesRepository
{
    public class HybridLinkRepository : IHybridLinkRepository
    {
        private readonly HybridAnalysisSettings _hybridAnalysisSettings;
        public HybridLinkRepository(HybridAnalysisSettings hybridAnalysisSettings)
        {
            _hybridAnalysisSettings = hybridAnalysisSettings;
        }
        public async Task<RestResponse> GetUrlResultAsync(string encodedUrl)
        {
            Console.WriteLine($"{_hybridAnalysisSettings.OverviewSha}{encodedUrl}");
            var options = new RestClientOptions($"{_hybridAnalysisSettings.OverviewSha}{encodedUrl}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key", _hybridAnalysisSettings.ApiKey);

            var response = await client.GetAsync(request);
            Console.WriteLine(response.Content);
            return response;
        }

        public async Task<RestResponse> PostUrlAsync(string url)
        {
            Console.WriteLine($"{_hybridAnalysisSettings.QuickScanUrl}");
            var options = new RestClientOptions(_hybridAnalysisSettings.QuickScanUrl);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("api-key", _hybridAnalysisSettings.ApiKey);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Console.WriteLine(url);
            request.AddBody($"scan_type=all&url={url}&comment=&submit_name=");

            var response = await client.PostAsync(request);
            Console.WriteLine(response.Content);
            return response;
        }
    }
}
