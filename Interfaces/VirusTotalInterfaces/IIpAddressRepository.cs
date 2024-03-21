using RestSharp;

namespace securityApp.Interfaces.VirusTotalInterfaces
{
    public interface IIpAddressRepository
    {
        Task<RestResponse> GetIpAddressResults(string ipAddress);
        bool isIpValid(string ipAddress);
    }
}
