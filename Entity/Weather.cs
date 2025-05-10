using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Entity
{
    public class Weather
    {
        [Required]
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public string? Condition { get; set; }

        [Required]
        public int CityId { get; set; } //adddddded
        public City? City { get; set; }
    }
}