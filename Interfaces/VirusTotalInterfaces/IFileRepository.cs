using RestSharp;

namespace securityApp.Interfaces.VirusTotalInterfaces
{
    public interface IFileRepository
    {
        Task<RestResponse> UploadFile(IFormFile file);
        Task<RestResponse> GetFileResult(string fileSHA);

    }
}
