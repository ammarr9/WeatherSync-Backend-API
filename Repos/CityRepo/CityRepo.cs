using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Entity;
using WeatherApp.DTOs.City;
using WeatherApp.DTOs.Forecast;
using WeatherApp.DTOs.Weather;
using WeatherApp.Repos.CityRepo;

namespace WeatherApp.Repos.City
{
    public class CityRepo : ICityRepo
    {
        private readonly ApplicationDbContext _context;
        public CityRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<CityResponseDto> GetAllCities()
        {
            var cities = _context.Cities
                .Include(c => c.Weather)
                .Include(c => c.Forecasts)
                .ToList();

            var cityDtos = cities?.Select(city => new CityResponseDto
            {
                Name = city.Name,
                Country = city.Country,
                WeatherDto = new WeatherRequestDto
                {
                    Temperature = (float)city.Weather.Temperature,
                    Humidity = (int)city.Weather.Humidity,
                    Condition = city.Weather.Condition
                },
                ForecastsDto = city.Forecasts.Select(forecast => new ForecastForCityDto
                {
                    Date = forecast.Date,
                    Temperature = forecast.Temperature,
                    Condition = forecast.Condition
                }).ToList()
            }).ToList() ?? new List<CityResponseDto>();

            return cityDtos;
        }

        public CityResponseDto GetCityById(int cityId)
        {
            var city = _context.Cities
                .Include(c => c.Weather)
                .Include(c => c.Forecasts)
                .FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                throw new Exception("City not found");
            }

            var cityDto = new CityResponseDto
            {
                Name = city.Name,
                Country = city.Country,
                WeatherDto = new WeatherRequestDto
                {
                    Temperature = (float)city.Weather.Temperature,//adddeddddd
                    Humidity = (int)city.Weather.Humidity,
                    Condition = city.Weather.Condition
                },
                ForecastsDto = city.Forecasts.Select(forecast => new ForecastForCityDto
                {
                    Date = forecast.Date,
                    Temperature = forecast.Temperature,
                    Condition = forecast.Condition
                }).ToList()
            };
            return cityDto;
        }

        public void AddCity(CityRequestDto cityRequestDto)
        {
            var city = new Entity.City // updatted
            {
                Name = cityRequestDto.Name,
                Country = cityRequestDto.Country
            };
            _context.Cities.Add(city);
            _context.SaveChanges();
        }
    }
}