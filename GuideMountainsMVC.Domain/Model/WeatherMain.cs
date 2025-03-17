using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class WeatherMain
    {
        public double Temp { get; set; } // Temperatura w stopniach Celsjusza
        public double Pressure { get; set; } // Ciśnienie atmosferyczne
        public double Humidity { get; set; } // Wilgotność powietrza
    }
}
