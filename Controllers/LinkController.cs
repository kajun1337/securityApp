using Microsoft.AspNetCore.Mvc;
using RestSharp;
using securityApp.Helper;


namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : Controller
    {
        private readonly VirusTotalSettings _settings;
        [HttpGet]
        public async Task<IActionResult> SendLink(string link)
        {

            var options = new RestClientOptions("https://www.virustotal.com/api/v3/urls");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-apikey", _settings.ApiKey);
            request.AddParameter("url", link);
            var response = await client.PostAsync(request);
            Console.WriteLine("{0}", response.Content);
            if (response.IsSuccessful)
            {
                Console.WriteLine("oldukine");
                return Ok(response);
                
            }
            return BadRequest(response);
            
        }
    }
}
