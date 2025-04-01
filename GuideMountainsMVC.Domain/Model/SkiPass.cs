using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class SkiPass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int MountainPlaceId { get; set; } // Zmieniono nazwę
        public MountainPlace MountainPlace { get; set; }
        public int CountryId { get; set; } // Zmieniono nazwę
        public Country Country { get; set; }
        public int SkiPassTypeId { get; set; }  // Klucz obcy do SkiPassType
        public SkiPassType SkiPassType { get; set; }
        public double BasePrice  { get; set; } // 💰 Dodaj to
    }
}

