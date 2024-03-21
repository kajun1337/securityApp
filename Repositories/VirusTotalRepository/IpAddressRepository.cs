using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces.VirusTotalInterfaces;
using System.Net;
using System.Text.RegularExpressions;

namespace securityApp.Repositories.VirusTotalRepository
{
    public class IpAddressRepository : IIpAddressRepository
    {
        private readonly VirusTotalSettings _virusTotalSettings;
        public IpAddressRepository(VirusTotalSettings virusTotalSettings)
        {
            _virusTotalSettings = virusTotalSettings;

        }
        public async Task<RestResponse> GetIpAddressResults(string ipAddress)
        {
            var options = new RestClientOptions($"{_virusTotalSettings.IpLink}{ipAddress}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _virusTotalSettings.ApiKey);
            var response = await client.GetAsync(request);

            Console.WriteLine("{0}", response.Content);
            return response;
        }

        public bool isIpValid(string ipAddress)
        {
            string pattern = @"^((([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5])\.){3}([0-1]?[0-9]{1,2}|2[0-4][0-9]|25[0-5]))$";
            return Regex.IsMatch(ipAddress, pattern);
        }
    }

}
