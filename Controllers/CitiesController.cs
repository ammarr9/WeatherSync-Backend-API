using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.DTOs.City;
using WeatherApp.Repos.CityRepo;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepo _cityRepo;

        public CitiesController(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }


        [HttpGet]
        public ActionResult<List<CityResponseDto>> GetCities()
        {
            var cities = _cityRepo.GetAllCities();
        
            return Ok(cities);
        }

        [HttpGet("{cityId}")]
        public ActionResult<CityResponseDto> GetCity(int cityId)
        {
            var city = _cityRepo.GetCityById(cityId);
            if (city == null)
            {
                return NotFound($"City with ID {cityId} not found.");
            }
            return Ok(city);
        }

        [HttpPost]
        public ActionResult AddCity([FromBody] CityRequestDto cityRequestDto)
        {
            if (cityRequestDto == null)
            {
                return BadRequest("Invalid data.");
            }

            _cityRepo.AddCity(cityRequestDto);

            return Created();
        }
    }
}