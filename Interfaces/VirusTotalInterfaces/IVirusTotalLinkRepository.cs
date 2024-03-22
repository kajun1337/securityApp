using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace securityApp.Interfaces.VirusTotalInterfaces
{
    public interface IVirusTotalLinkRepository
    {
        Task<RestResponse> PostUrlScanAsync(string url);
        Task<RestResponse> GetUrlScanResultAsync(string encodedUrl);

    }
}
