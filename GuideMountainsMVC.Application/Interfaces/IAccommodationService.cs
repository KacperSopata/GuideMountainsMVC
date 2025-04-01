using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IAccommodationService
    {
        ListAccommodationVm GetAllAccommodations(int pageSize, int currentPage, string searchString);
        AccommodationDetailVm GetAccommodationDetail(int id);
        int AddAccommodation(NewAccommodationVm newAccommodation);
        void UpdateAccommodation(NewAccommodationVm updatedAccommodation);
        void DeleteAccommodation(int id);
        ListAccommodationVm GetAccommodationByCountryId (int countryId);
        ListAccommodationVm GetAccommodationsByMountainPlaceId(int mountainPlaceId);
    }
}
