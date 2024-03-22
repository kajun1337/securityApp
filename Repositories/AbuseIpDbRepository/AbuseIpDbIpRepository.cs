using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces.AbuseIpDbInterfaces;

namespace securityApp.Repositories.AbuseIpDbRepository
{
    public class AbuseIpDbIpRepository : IAbuseIpDbIpRepository
    {
        private readonly AbuseIpDbSettings _settings;
        public AbuseIpDbIpRepository(AbuseIpDbSettings abuseIpDbSettings)
        {
            _settings = abuseIpDbSettings;
        }
        public async Task<RestResponse> GetAbuseIpDbIpResults(string ipAddress)
        {
            var options = new RestClientOptions("https://api.abuseipdb.com/api/v2/check");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("Key", _settings.ApiKey);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("ipAddress", ipAddress);
            request.AddParameter("maxAgeInDays", "90");
            request.AddParameter("verbose", "");

            var response = await client.GetAsync(request);

            return response;
        }
    }
}
