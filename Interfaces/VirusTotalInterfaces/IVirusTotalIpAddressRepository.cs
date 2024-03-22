using RestSharp;

namespace securityApp.Interfaces.VirusTotalInterfaces
{
    public interface IVirusTotalIpAddressRepository
    {
        Task<RestResponse> GetIpAddressResults(string ipAddress);
        bool isIpValid(string ipAddress);
    }
}
