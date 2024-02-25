using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Interfaces;
using securityApp.Helper;

namespace securityApp.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private VirusTotalSettings _totalSettings;
        public async Task<RestResponse> GetUrlScanResultAsync(string encodedUrl)
        {
            var options = new RestClientOptions($"https://www.virustotal.com/api/v3/urls/{encodedUrl}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            var response = await client.GetAsync(request);
            return response;
        }

        public async Task<RestResponse> PostUrlScanAsync(string url)
        {
            var options = new RestClientOptions("https://www.virustotal.com/api/v3/urls");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _totalSettings.ApiKey);
            request.AddParameter("url", url);
            var response = await client.PostAsync(request);
            return response;
        }
    }
}
