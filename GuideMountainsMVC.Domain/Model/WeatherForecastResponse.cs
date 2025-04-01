using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model.GuideMountainsMVC.Domain.Model;

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
        public long Dt { get; set; } // Czas, dla którego przewidywana jest pogoda
    }
}
