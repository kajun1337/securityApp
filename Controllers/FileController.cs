using Microsoft.AspNetCore.Mvc;
using securityApp.Helper;
using securityApp.Interfaces;
using System.Data;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : Controller
    {
        private readonly IFileRepository _fileRepository;
        private readonly Encoder _encoder;
        private readonly VirusTotalSettings _virusTotalSettings;
        public FileController(IFileRepository fileRepository, Encoder encoder, VirusTotalSettings virusTotalSettings)
        {
            _encoder = encoder;
            _fileRepository = fileRepository;
            _virusTotalSettings = virusTotalSettings;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _fileRepository.UploadFile(file);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetResultsOfFile(IFormFile file)
        {
            var temp = _encoder.EncodeFileToSHA265(file);
            var response = _fileRepository.GetFileResult(temp);
            return Ok(response);

        }
    }
}
