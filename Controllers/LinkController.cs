using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;
using securityApp.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using securityApp.Interfaces.VirusTotalInterfaces;
using securityApp.Interfaces.IHybridAnalysesRepository;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private VirusTotalSettings _totalSettings;
        private readonly IVirusTotalLinkRepository _linkRepository;
        private readonly IHybridLinkRepository _hybridLinkRepository;
        private readonly Encoder _encoder;


        public LinkController(VirusTotalSettings virusTotalSettings, IVirusTotalLinkRepository linkRepository,
            IHybridLinkRepository hybridLinkRepository ,Encoder encoder)
        {
            _totalSettings = virusTotalSettings;
            _linkRepository = linkRepository;
            _hybridLinkRepository = hybridLinkRepository;
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

        [HttpPost]
        [Route("PostHybridLink")]

        public async Task<IActionResult> PostHybridLink(string link)
        {
            var result = _hybridLinkRepository.PostUrlAsync(link);
            return Ok(result);
        }
    }
}
