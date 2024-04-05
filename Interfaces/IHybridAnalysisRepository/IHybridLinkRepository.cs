using RestSharp;

namespace securityApp.Interfaces.IHybridAnalysesRepository
{
    public interface IHybridLinkRepository
    {
        Task<RestResponse> PostUrlAsync(string url);
        Task<RestResponse> GetUrlResultAsync(string encodedUrl);
        string GetShaFromResponse(RestResponse response);
    }
}
