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
        [Route("Vt-SendLink")]
        public async Task<IActionResult> SendLink(string link)
        {
            var result = await _linkRepository.PostUrlScanAsync(link);
            return Ok();
        }

        [HttpGet]
        [Route("Vt-GetLinkResult")]
        public async Task<IActionResult> GetLinkResult(string link)
        {
            var encodedUrl = _encoder.EncodeUrlToBase64(link);
            var response = await _linkRepository.GetUrlScanResultAsync(encodedUrl);
            Console.WriteLine(response.Content);
            return Ok(response.Content);

        }

        [HttpPost]
        [Route("Ha-SendLink")]

        public async Task<IActionResult> PostHybridLink(string link)
        {
            var result = await _hybridLinkRepository.PostUrlAsync(link);
            return Ok(result.Content);
        }

        [HttpGet]
        [Route("Ha-GetLinkResult")]

        public async Task<IActionResult> GetHybridLinkResult(string link)
        {
            var encodedLink = _encoder.LinkToSha256(link);
            Console.WriteLine(encodedLink);
            var response = await _hybridLinkRepository.GetUrlResultAsync(encodedLink);
            return Ok(response);
        }
    }
}
