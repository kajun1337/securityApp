﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _fileRepository.UploadFile(file);
            return Ok();
        }

        [HttpGet]
        [Route("GetFileResults")]
        public async Task<IActionResult> GetResultsOfFile(string encodedFileSha256)
        {
            var response = await _fileRepository.GetFileResult(encodedFileSha256);
            return Ok(response.Content);
        }

    }
}
