using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class Country
    {
        public int Id { get; set; } // Unikalny identyfikator kraju
        public string Name { get; set; } // Nazwa kraju
        public ICollection<MountainPlace> MountainPlaces { get; set; } // Lista powiązanych miejsc w górach
        public ICollection<SkiPass> SkiPasses { get; set; } // Lista powiązanych miejsc w górach
        public ICollection<Accommodation> Accommodations { get; set; } // Lista powiązanych miejsc w górach
    }

}
