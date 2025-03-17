using GuideMountainsMVC.Application.ViewModel.CountryVm;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IMountainPlaceService
    {
        ListMountainPlaceVm GetAllMountainPlacesForList(int pageSize, int pageNo, string searchString);

        MountainPlaceDetailVm GetMountainPlaceDetails(int id);

        NewMountainPlaceVm GetMountainPlaceForEdit(int id);

        int AddMountainPlace(NewMountainPlaceVm model);

        void UpdateMountainPlace(NewMountainPlaceVm model);

        void DeleteMountainPlace(int id);
        public List<CountryForListVm> GetAllCountries();
        public ListMountainPlaceVm GetMountainPlacesByCountryId(int countryId);
        ListMountainPlaceVm GetAllMountainPlacesForCountry(int countryId);

        List<MountainPlace> GetAllMountainPlaces(); // Dodajemy metodę do pobierania wszystkich miejsc

        MountainPlace GetMountainPlaceById(int mountainPlaceId); // Dodajemy metodę do pobierania miejsca po ID
    }

}
