using RestSharp;

namespace securityApp.Interfaces
{
    public interface IFileRepository
    {
        Task<RestResponse> UploadFile(IFormFile file);
        Task<RestResponse> GetFileResult(string fileSHA);
    }
}
