using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysesRepository;

namespace securityApp.Repositories.HybridAnalysesRepository
{
    public class HybridLinkRepository : IHybridLinkRepository
    {
        private readonly HybridAnalysisSettings _hybridAnalysisSettings;
        public HybridLinkRepository(HybridAnalysisSettings hybridAnalysisSettings)
        {
            _hybridAnalysisSettings = hybridAnalysisSettings;
        }
        public Task<RestResponse> GetUrlResultAsync(string encodedUrl)
        {
            throw new NotImplementedException();
        }

        public async Task<RestResponse> PostUrlAsync(string url)
        {
            var options = new RestClientOptions(_hybridAnalysisSettings.QuickScanUrl);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Bearer{_hybridAnalysisSettings.ApiKey}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            Console.WriteLine(url);
            request.AddParameter("scan_type", "all");
            request.AddParameter("url", url);
            request.AddParameter("comment", "");
            request.AddParameter("submit_name", "");
            Console.WriteLine(request.ToString());
            var response = await client.PostAsync(request);
            Console.WriteLine(response.Content);
            return response;
        }
    }
}
