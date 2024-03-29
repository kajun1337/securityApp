using Microsoft.AspNetCore.Mvc;
using securityApp.Helper;
using securityApp.Interfaces.VirusTotalInterfaces;
using System.Data;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private const string folderName = "FilesToUpload";
        private readonly string folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        private readonly IVirusTotalFileRepository _virusTotalFileRepository;
        private readonly Encoder _encoder;
        private readonly VirusTotalSettings _virusTotalSettings;
        public FileController(IVirusTotalFileRepository fileRepository, Encoder encoder, VirusTotalSettings virusTotalSettings)
        {
            _encoder = encoder;
            _virusTotalFileRepository = fileRepository;
            _virusTotalSettings = virusTotalSettings;
        }
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            Console.WriteLine(_encoder.EncodeFileToSHA265(file));
            var result = await _virusTotalFileRepository.UploadFile(file);
            Console.WriteLine(result.Content);
            return Ok(result.Content);
        }

        [HttpGet]
        [Route("GetFileResults")]
        public async Task<IActionResult> GetResultsOfFile(string encodedFileSha256)
        {

            var response = await _virusTotalFileRepository.GetFileResult(encodedFileSha256);
            Console.WriteLine(response.Content);

            return Ok(response.Content);
        }

    }
}
