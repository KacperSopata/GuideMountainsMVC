using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    namespace GuideMountainsMVC.Domain.Model
    {
        public class Weather
        {
            public int Id { get; set; }
            public string Main { get; set; } // Przykład: "Clear", "Clouds"
            public string Description { get; set; } // Przykład: "Clear sky", "Few clouds"
            public string Icon { get; set; } // Przykład: "01d", "02d"
        }
    }

}
