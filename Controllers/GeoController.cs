using Geolocalizacion.Modelos;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;

namespace Geolocalizacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GeoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet("[action]/{ipAddress}")]
        public IActionResult GetCountry(string ipAddress)
        {
            string strRuta = _configuration.GetValue<string>("ConnectionStrings:pathDB");
            try
            {
                using var reader = new DatabaseReader(strRuta);

                var response = reader.Country(ipAddress);

                var geoLocation = new GeoClass
                {
                    countryName = response.Country.Name,
                    countryIsoCode = response.Country.IsoCode,
                    IsInEuropeanUnion = response.Country.IsInEuropeanUnion
                };

                return StatusCode(StatusCodes.Status200OK, geoLocation);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, ex.Message);
            }




        }
    }
}
