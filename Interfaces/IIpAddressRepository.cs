using RestSharp;

namespace securityApp.Interfaces
{
    public interface IIpAddressRepository
    {
        Task<RestResponse> GetIpAddressResults(string ipAddress);
        bool isIpValid(string ipAddress);
    }
}
