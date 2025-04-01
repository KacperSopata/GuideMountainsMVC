using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class SkiPassType
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Normalny, Ulgowy, itp.
        public double PriceMultiplier { get; set; } // Współczynnik ceny np. 1.0 dla normalnego, 0.8 dla ulgowego
        public ICollection<SkiPass> SkiPasses { get; set; } // Relacja do SkiPass
    }
}
