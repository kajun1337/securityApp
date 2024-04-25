using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;
using securityApp.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using securityApp.Interfaces.VirusTotalInterfaces;
using securityApp.Interfaces.IHybridAnalysesRepository;
using System.Runtime.CompilerServices;


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
        private string _sha256;  


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
        public async Task<IActionResult> SendVtLink(string link)
        {
            var result = await _linkRepository.PostUrlScanAsync(link);
            return Ok();
        }

        [HttpGet]
        [Route("Vt-GetLinkResult")]
        public async Task<IActionResult> GetVtLinkResult(string link)
        {
            var encodedUrl = _encoder.EncodeUrlToBase64(link);
            var response = await _linkRepository.GetUrlScanResultAsync(encodedUrl);
            //Console.WriteLine(response.Content);
            return Ok(response.Content);

        }

        [HttpPost]
        [Route("Ha-SendLink")]

        public async Task<IActionResult> PostHybridLink(string link)
        {
            var result = await _hybridLinkRepository.PostUrlAsync(link);
            //JObject content = Json.Parse(result.Content);
            //if (content["finished"])
            return Ok(result.Content);
        }

        [HttpGet]
        [Route("Ha-GetLinkResult")]

        public async Task<IActionResult> GetHybridLinkResult(string link)
        {   
            var encodedUrl = _encoder.LinkToSha256(link);
            Console.WriteLine(encodedUrl);
            var response = await _hybridLinkRepository.GetUrlResultAsync(encodedUrl);
            return Ok(response.Content);
        }
    }
}
