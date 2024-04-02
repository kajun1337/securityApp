using RestSharp;
namespace securityApp.Interfaces.IHybridAnalysisRepository
{
    public interface IHybridFileRepository
    {
        Task<RestResponse> SendFile(IFormFile file);
        Task<RestResponse> GetFileReport(string sha256);
    }
}
