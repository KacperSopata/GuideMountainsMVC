using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IEquipmentRentalService
    {
        ListEquipmentRentalVm GetAllEquipmentRentals(int pageSize, int currentPage, string searchString);
        EquipmentRentalDetailVm GetEquipmentRentalDetail(int id);
        int AddEquipmentRental(NewEquipmentRentalVm newEquipmentRental);
        void UpdateEquipmentRental(NewEquipmentRentalVm updatedEquipmentRental);
        void DeleteEquipmentRental(int id);
        ListEquipmentRentalVm GetEquipmentRentalsByCategoryId(int categoryForeignKey);
        List<CategoryEquipmentRental> GetAllCategories();
        ListEquipmentRentalVm GetEquipmentRentalsByCountryId(int countryId);
        ListEquipmentRentalVm GetEquipmentRentalsByMountainPlaceId(int mountainPlaceId);
        EquipmentRentalBasicVm GetEquipmentRentalById(int id);
        List<MountainPlaceForListVm> GetMountainPlacesForCountry(int countryId);
        List<EquipmentRentalForListVm> GetEquipmentRentalsForMountainPlace(int mountainPlaceId);

    }
}
