using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Entity
{
    public class City
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Country { get; set; }
        public Weather Weather { get; set; } = new Weather(); // adddeddd
        public List<Forecast> Forecasts { get; set; } = new List<Forecast>(); //addedd
    }
}