using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface IEquipmentRentalRepository
    {
        IQueryable<EquipmentRental> GetAllEquipmentRentals();
        int AddEquipmentRental(EquipmentRental equipmentRental);
        EquipmentRental GetEquipmentRentalById(int id);
        void DeleteEquipmentRental(EquipmentRental equipmentRental);
        void UpdateEquipmentRental(EquipmentRental equipmentRental);
        IEnumerable<EquipmentRental> GetByCategoryId(int categoryForeignKey);
        IEnumerable<EquipmentRental> GetByCountryId(int categoryForeignKey);
        IEnumerable<CategoryEquipmentRental> GetAllCategories();
        IEnumerable<EquipmentRental> GetEquipmentRentalsByMountainPlaceId(int mountainPlaceId);
        EquipmentRental GetById(int id);
        IEnumerable<MountainPlace> GetMountainPlacesByCountryId(int countryId);

    }
}
