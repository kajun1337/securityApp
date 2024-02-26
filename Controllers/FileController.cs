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
            
        }
    }
}
