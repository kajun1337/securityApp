using RestSharp;

namespace securityApp.Interfaces.AbuseIpDbInterfaces
{
    public interface IAbuseIpDbIpRepository
    {
        Task<RestResponse> GetAbuseIpDbIpResults(string ipAddress);
    }
}
