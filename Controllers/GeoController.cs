using Geolocalizacion.Modelos;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Geolocalizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        [HttpGet("[action]/{ipAddress}")]
        public IActionResult GetCountry(string ipAddress)
        {            
            using var reader = new DatabaseReader(@"C:\Desarrollo\Geo-Location-API-Net-Core-master\GeoLite2-Country.mmdb");

            var response = reader.Country(ipAddress);

            var geoLocation = new GeoClass
            {
                countryName = response.Country.Name,
                countryIsoCode = response.Country.IsoCode,
                IsInEuropeanUnion = response.Country.IsInEuropeanUnion
            };

            return StatusCode(StatusCodes.Status200OK, geoLocation);


        }
    }
}
