using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.DTOs.City;
using WeatherApp.DTOs.Forecast;
using WeatherApp.Repos.CityRepo;
using WeatherApp.Repos.ForecastRepo;

namespace WeatherApp.Repos.Forecast
{
    public class ForecastRepo : IForecastRepo
    {
        private readonly ICityRepo _repo;

        private readonly ApplicationDbContext _context;
        public ForecastRepo(ApplicationDbContext context, ICityRepo repo)
        {
            _context = context;
            _repo = repo; //addeddd
        }

        public List<ForecastResponseDto> GetForecastsByCityId(int cityId)
        {
            var forecasts = _context.Forecasts
                .Where(f => f.CityId == cityId)
                .Select(f => new ForecastResponseDto
                {
                    Id = f.Id,
                    CityId = f.CityId,
                    Date = f.Date,
                    Temperature = f.Temperature,
                    Condition = f.Condition
                })
                .ToList();

            return forecasts;
        }

        public void AddForecast(ForecastRequestDto forecastRequestDto)
        {
            var forecast = new Entity.Forecast
            {
                CityId = forecastRequestDto.CityId,
                Date = forecastRequestDto.Date,
                Temperature = forecastRequestDto.Temperature,
                Condition = forecastRequestDto.Condition
            };
            _context.Forecasts.Add(forecast); //addedd
            _context.SaveChanges();
        }

        public void DeleteForecast(int id)
        {
            var forecast = _context.Forecasts.FirstOrDefault(f => f.Id == id);
            if (forecast != null)
            {
                _context.Remove(forecast); // addedd
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Forecast with ID {id} not found.");
            }
        }

    }
}

