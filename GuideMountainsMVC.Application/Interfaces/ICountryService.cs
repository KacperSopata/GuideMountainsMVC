using GuideMountainsMVC.Application.ViewModel.CountryVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface ICountryService
    {
        public List<Country> GetAllCountries();
        CountryDetailVm GetCountryDetail(int id);
        void AddCountry(NewCountryVm newCountry);
        void UpdateCountry(NewCountryVm updatedCountry);
        void DeleteCountry(int id);
        Country GetCountryById(int id);
        IEnumerable<SelectListItem> GetCountriesForDropdown();  // Pobiera listę krajów
    }
}
