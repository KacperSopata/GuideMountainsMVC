using GuideMountainsMVC.Domain.Model.GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class WeatherForecastResponse
    {
        public List<Forecast> List { get; set; } // Lista prognoz
    }

    public class Forecast
    {
        public WeatherMain Main { get; set; } // Dane pogodowe dla danego punktu czasowego
        public List<Weather> Weather { get; set; } // Opis pogody
        public DateTime Dt { get; set; } // Czas, dla którego przewidywana jest pogoda
    }
}
