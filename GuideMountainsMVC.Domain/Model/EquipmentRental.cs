using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class EquipmentRental
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int MountainPlaceId { get; set; }
        public MountainPlace MountainPlace { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int CategoryEquipmentRentalId { get; set; }
        public double PricePerDay { get; set; }
        public CategoryEquipmentRental CategoryEquipmentRental { get; set; }
    }


}
