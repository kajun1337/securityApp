using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;
using securityApp.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using securityApp.Interfaces.VirusTotalInterfaces;


namespace securityApp.Controllers.VirusTotalControllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private VirusTotalSettings _totalSettings;
        private readonly ILinkRepository _linkRepository;
        private readonly Encoder _encoder;


        public LinkController(VirusTotalSettings virusTotalSettings, ILinkRepository linkRepository, Encoder encoder)
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

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Task.Delay(3000);
                return await GetLinkResult(link);
            }
            else
            {
                JObject result = JObject.Parse(response.Content);
                var lastAnalysisResult = result["data"]["attributes"]["last_analysis_results"];
                if (lastAnalysisResult.ToString() == "{}")
                {
                    await Task.Delay(3000);
                    return await GetLinkResult(link);
                }
            }
            Console.WriteLine(response.Content);
            return Ok(response.Content);

        }
    }
}
