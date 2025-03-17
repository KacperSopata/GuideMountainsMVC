using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class MountainPlace
    {
        public int Id { get; set; } // Unikalny identyfikator miejsca
        public string Name { get; set; } // Nazwa miejsca
        public string Description { get; set; } // Opis miejsca
        public int CountryId { get; set; } // Klucz obcy do Country
        public Country Country { get; set; } // Nawigacja do powiązanego kraju
        public ICollection<SkiPass> SkiPasses { get; set; } // Lista powiązanych SkiPassów
        public ICollection<Accommodation> Accommodations { get; set; } // Lista dostępnych miejsc noclegowych
    }

}
