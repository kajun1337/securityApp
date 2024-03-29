using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using securityApp.Helper;
using securityApp.Interfaces.AbuseIpDbInterfaces;
using securityApp.Interfaces.VirusTotalInterfaces;
using System.Net;

namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpAddressController : Controller
    {
        private readonly IVirusTotalIpAddressRepository _virusTotalIpAddressRepository;
        private readonly IAbuseIpDbIpRepository _abuseIpDbIpRepository;
        public IpAddressController(IVirusTotalIpAddressRepository ipAddressRepository, IAbuseIpDbIpRepository abuseIpDbIpRepository)
        {
            _virusTotalIpAddressRepository = ipAddressRepository;
            _abuseIpDbIpRepository = abuseIpDbIpRepository;
               
        }
        [HttpGet]
        [Route("getVirusTotalIpAddressResult")]
        public async Task<IActionResult> GetVirusTotalIpAddressResult(string ipAddress)
        {
            Console.WriteLine(ipAddress);
            Console.WriteLine(_virusTotalIpAddressRepository.isIpValid(ipAddress));
            if (!_virusTotalIpAddressRepository.isIpValid(ipAddress))
            {
                return BadRequest();
            }
            var response = await _virusTotalIpAddressRepository.GetIpAddressResults(ipAddress);
            return Ok(response.Content);
        }

        [HttpGet]
        [Route("getIpDbIpAddressResult")]

        public async Task<IActionResult> getIpDbIpAddressResult(string ipAddress)
        {
            if (!_virusTotalIpAddressRepository.isIpValid(ipAddress))
            {            
                return BadRequest();
            }

            var response = await _abuseIpDbIpRepository.GetAbuseIpDbIpResults(ipAddress);
            dynamic parsedJson = JsonConvert.DeserializeObject(response.Content);

            foreach (var item in parsedJson)
            {
                Console.WriteLine(item);
            }
            return Ok(response.Content);
        }
    }
}
