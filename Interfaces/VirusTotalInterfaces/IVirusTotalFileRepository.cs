using RestSharp;

namespace securityApp.Interfaces.VirusTotalInterfaces
{
    public interface IVirusTotalFileRepository
    {
        Task<RestResponse> UploadFile(IFormFile file);
        Task<RestResponse> GetFileResult(string fileSHA);

    }
}
