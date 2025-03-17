using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class WeatherHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; } // ID użytkownika (opcjonalne, jeśli logowanie)
        public string City { get; set; }
        public double Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public double? Snow1h { get; set; }
        public double? Snow3h { get; set; }
        public double? Snow24h { get; set; }
        public DateTime QueryDate { get; set; } = DateTime.UtcNow; // Data zapytania
    }
}
