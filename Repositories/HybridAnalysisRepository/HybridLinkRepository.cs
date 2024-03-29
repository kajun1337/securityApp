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
            Console.WriteLine($"{_hybridAnalysisSettings.OverviewUrl}{encodedUrl}");
            var options = new RestClientOptions($"{_hybridAnalysisSettings.OverviewUrl}{encodedUrl}");
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
            request.AddHeader("content-Type", "application/x-www-form-urlencoded");
            Console.WriteLine(url);
            request.AddParameter("scan_type", "all");
            request.AddParameter("url", url);
            request.AddParameter("comment", "");
            request.AddParameter("submit_name", "");
            Console.WriteLine(request.ToString());
            try
            {
                var response = await client.PostAsync(request);
                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            
            return null;
        }
    }
}
