using Microsoft.AspNetCore.Mvc;
using securityApp.Interfaces;
using System.Net;

namespace securityApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IpAddressController : Controller
    {
        private readonly IIpAddressRepository _ipAddressRepository;
        public IpAddressController(IIpAddressRepository ipAddressRepository)
        {
            _ipAddressRepository = ipAddressRepository;
        }
        [HttpGet]
        [Route("getIpAddressResult")]
        public async Task<IActionResult> GetIpAddressResul(string ipAddress)
        {
            Console.WriteLine(ipAddress);
            Console.WriteLine(_ipAddressRepository.isIpValid(ipAddress));
            if(!_ipAddressRepository.isIpValid(ipAddress))
            {
                return BadRequest();
            }
            var response = await _ipAddressRepository.GetIpAddressResults(ipAddress);
            return Ok(response);
        }
    }
}
