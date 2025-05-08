using WeatherApp.Data;
using WeatherApp.DTOs.Weather;
using WeatherApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Repos.WeatherRepo
{
    public class WeatherRepo : IWeatherRepo
    {
        private readonly ApplicationDbContext _context;

        public WeatherRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddWeather(WeatherRequestDto weatherRequestDto)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Id == weatherRequestDto.CityId);
            if (city == null)
            {
                throw new Exception($"City with ID {weatherRequestDto.CityId} not found.");
            }

            var weather = new Entity.Weather
            {
                Temperature = weatherRequestDto.Temperature,
                Humidity = weatherRequestDto.Humidity,
                WindSpeed = weatherRequestDto.WindSpeed,
                Condition = weatherRequestDto.Condition,
                CityId = weatherRequestDto.CityId
            };
            _context.Weathers.Add(weather);
            _context.SaveChanges();
        }

        public WeatherResponseDto? GetWeatherByCityId(int cityId)
        {
            var weather = _context.Weathers
                .Where(w => w.CityId == cityId)
                .Select(w => new WeatherResponseDto
                {
                    Id = w.Id,
                    Temperature = w.Temperature,
                    Humidity = w.Humidity,
                    WindSpeed = w.WindSpeed,
                    Condition = w.Condition
                })
                .FirstOrDefault();
            return weather;
        }

        public void UpdateWeather(int id, WeatherRequestDto weatherDto)
        {
            var weather = _context.Weathers.FirstOrDefault(w => w.Id == id);

            if (weather == null)
            {
                throw new Exception($"Weather with ID {id} not found.");
            }

            weather.Temperature = weatherDto.Temperature;
            weather.Humidity = weatherDto.Humidity;
            weather.WindSpeed = weatherDto.WindSpeed;
            weather.Condition = weatherDto.Condition;

            _context.Weathers.Update(weather);
            _context.SaveChanges();
        }
    }
}