using GuideMountainsMVC.Domain.Model.GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class WeatherResponse
    {
        public string Name { get; set; } // Nazwa miasta
        public WeatherMain Main { get; set; } // Dane o temperaturze, wilgotności
        public List<Weather> Weather { get; set; } // Opis pogody
        public Snow Snow { get; set; } // Nowe dane o śniegu
    }

}
