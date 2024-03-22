using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;
using securityApp.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using securityApp.Interfaces.VirusTotalInterfaces;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private VirusTotalSettings _totalSettings;
        private readonly IVirusTotalLinkRepository _linkRepository;
        private readonly Encoder _encoder;


        public LinkController(VirusTotalSettings virusTotalSettings, IVirusTotalLinkRepository linkRepository, Encoder encoder)
        {
            _totalSettings = virusTotalSettings;
            _linkRepository = linkRepository;
            _encoder = encoder;
        }


        [HttpPost]
        [Route("SendLink")]
        public async Task<IActionResult> SendLink(string link)
        {
            var result = await _linkRepository.PostUrlScanAsync(link);
            return Ok();
        }

        [HttpGet]
        [Route("GetLinkResult")]
        public async Task<IActionResult> GetLinkResult(string link)
        {
            var encodedUrl = _encoder.EncodeUrlToBase64(link);
            var response = await _linkRepository.GetUrlScanResultAsync(encodedUrl);
            Console.WriteLine(response.Content);
            return Ok(response.Content);

        }
    }
}
