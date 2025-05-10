using System.ComponentModel.DataAnnotations;

namespace WeatherApp.DTOs.Weather
{
    public class WeatherResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public double Temperature { get; set; }

        [Required]
        public double Humidity { get; set; }

        [Required]
        public double WindSpeed { get; set; }

        [Required]
        public string? Condition { get; set; }
    }
}
