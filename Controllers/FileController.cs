using Microsoft.AspNetCore.Mvc;
using securityApp.Helper;
using securityApp.Interfaces.IHybridAnalysisRepository;
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

        private readonly IHybridFileRepository _hybridFileRepository;
        private readonly IVirusTotalFileRepository _virusTotalFileRepository;
        private readonly Encoder _encoder;
        private readonly VirusTotalSettings _virusTotalSettings;

        public FileController(IVirusTotalFileRepository fileRepository, Encoder encoder, VirusTotalSettings virusTotalSettings, IHybridFileRepository hybridFileRepository)
        {
            _encoder = encoder;
            _virusTotalFileRepository = fileRepository;
            _hybridFileRepository = hybridFileRepository;
            _virusTotalSettings = virusTotalSettings;
        }
        [HttpPost]
        [Route("VT-UploadFile")]
        public async Task<IActionResult> VtUploadFile(IFormFile file)
        {
            Console.WriteLine(_encoder.EncodeFileToSHA265(file));
            var result = await _virusTotalFileRepository.UploadFile(file);
            Console.WriteLine(result.Content);
            return Ok(result.Content);
        }

        [HttpGet]
        [Route("VT-GetFileResults")]
        public async Task<IActionResult> VTGetResultsOfFile(string encodedFileSha256)
        {

            var response = await _virusTotalFileRepository.GetFileResult(encodedFileSha256);
            Console.WriteLine(response.Content);

            return Ok(response.Content);
        }

        [HttpPost]
        [Route("Ha-UploadFile")]

        public async Task<IActionResult> PostHybridFile(IFormFile file)
        {
            var response = await _hybridFileRepository.SendFile(file);

            return Ok(response.Content);
        }
    }
}
