using System;
using System.Collections.Generic;

namespace GuideMountainsMVC.Domain.Model
{
    public class WeatherFiveDayForecast
    {
        public string CityName { get; set; } // Nazwa miasta
        public List<DailyForecast> Forecasts { get; set; } // Lista prognoz na 5 dni
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; } // Data prognozy
        public double MinTemp { get; set; } // Minimalna temperatura w ciągu dnia
        public double MaxTemp { get; set; } // Maksymalna temperatura w ciągu dnia
        public string WeatherDescription { get; set; } // Opis pogody (np. "Deszczowo", "Słonecznie")
        public string Icon { get; set; } // Ikona pogody (np. "01d")
    }
}
