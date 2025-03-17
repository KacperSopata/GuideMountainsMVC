using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.CountryVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Application.ViewModel.CountryVm;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuideMountainsMVC.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public List<Country> GetAllCountries()
        {
            // Zamiast _context, użyj _countryRepository
            return _countryRepository.GetAllCountries(); // Zwróć wszystkie kraje z repozytorium
        }

        public CountryDetailVm GetCountryDetail(int id)
        {
            var country = _countryRepository.GetCountryById(id);
            return _mapper.Map<CountryDetailVm>(country);
        }

        public void AddCountry(NewCountryVm newCountry)
        {
            var country = _mapper.Map<Country>(newCountry);
            _countryRepository.AddCountry(country);
        }

        public void UpdateCountry(NewCountryVm updatedCountry)
        {
            var country = _mapper.Map<Country>(updatedCountry);
            _countryRepository.UpdateCountry(country);
        }

        public void DeleteCountry(int id)
        {
            var country = _countryRepository.GetCountryById(id);
            if (country != null)
            {
                _countryRepository.DeleteCountry(country);
            }
        }
        public Country GetCountryById(int id)
        {
            return _countryRepository.GetCountryById(id); // Wykonujemy odpowiednią logikę lub zapytanie do bazy
        }
        public IEnumerable<SelectListItem> GetCountriesForDropdown()
        {
            var countries = _countryRepository.GetAllCountries();  // Pobieramy wszystkie kraje z repozytorium
            return countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }
    }
}
