using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace securityApp.Interfaces
{
    public interface ILinkRepository
    {
        Task<RestResponse> PostUrlScanAsync(string url);
        Task<RestResponse> GetUrlScanResultAsync(string encodedUrl);
        
    }
}
