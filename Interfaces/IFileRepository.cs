using RestSharp;

namespace securityApp.Interfaces
{
    public interface IFileRepository
    {
        Task<RestResponse> SendFile(IFormFile file);
        Task<RestResponse> GetFileResult(string fileSHA);
    }
}
