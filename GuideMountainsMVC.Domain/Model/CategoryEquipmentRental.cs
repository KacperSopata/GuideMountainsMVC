using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class CategoryEquipmentRental
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EquipmentRental> EquipmentRentals { get; set; }
    }
}
