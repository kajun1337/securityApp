using RestSharp;
using static System.Net.WebRequestMethods;
namespace securityApp.Helper
{
    public class VirusTotalSettings
    {
        public string ApiKey = "9e29d12faea87e993c8ad7a07bafeb4ce62827063e79251651420aed6bb49047";
        public string FileLink = "https://www.virustotal.com/api/v3/files";
        public string UrlLink = "https://www.virustotal.com/api/v3/urls";
        public string IpLink = "https://www.virustotal.com/api/v3/ip_addresses/";
    }
}
