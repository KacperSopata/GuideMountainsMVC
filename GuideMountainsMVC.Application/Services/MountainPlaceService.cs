using AutoMapper;
using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.CountryVm;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace GuideMountainsMVC.Application.Services
{
    public class MountainPlaceService : IMountainPlaceService
    {
        private readonly IMountainPlaceRepository _mountainPlaceRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ISkiPassRepository _skiRepository;
        private readonly IMapper _mapper;


        public MountainPlaceService(IMountainPlaceRepository mountainPlaceRepository, ICountryRepository countryRepository, ISkiPassRepository skiPassRepository, IMapper mapper)
        {
            _mountainPlaceRepository = mountainPlaceRepository;
            _countryRepository = countryRepository;
            _skiRepository = skiPassRepository;
            _mapper = mapper;

        }

        // Pobierz wszystkie miejsca górskie z paginacją i możliwością wyszukiwania
        public ListMountainPlaceVm GetAllMountainPlacesForList(int pageSize, int pageNo, string searchString)
        {
            var placesQuery = _mountainPlaceRepository.GetAll();  // Pobieramy wszystkie miejsca

            // Filtrujemy po nazwie miejsca, jeśli podano wyszukiwaną frazę
            if (!string.IsNullOrEmpty(searchString))
            {
                placesQuery = placesQuery.Where(mp => mp.Name.Contains(searchString));
            }

            // Zastosowanie paginacji
            var places = placesQuery
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Tworzymy model do zwrócenia
            var model = new ListMountainPlaceVm
            {
                MountainPlaces = places.Select(mp => new MountainPlaceForListVm
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    CountryId = mp.CountryId,

                }).ToList(),
                Count = placesQuery.Count(),
                CurrentPage = pageNo,
                PageSize = pageSize,
                SearchString = searchString
            };

            return model;
        }

        // Pobierz szczegóły miejsca górskiego
        public MountainPlaceDetailVm GetMountainPlaceDetails(int id)
        {
            var mountainPlace = _mountainPlaceRepository.GetById(id);
            if (mountainPlace != null)
            {
                return new MountainPlaceDetailVm
                {
                    Id = mountainPlace.Id,
                    Name = mountainPlace.Name,
                    Description = mountainPlace.Description,
                    CountryId = mountainPlace.CountryId,
                };
            }

            return null;  // Jeśli nie znaleziono miejsca
        }

        // Pobierz dane miejsca górskiego do edycji
        public NewMountainPlaceVm GetMountainPlaceForEdit(int id)
        {
            var mountainPlace = _mountainPlaceRepository.GetById(id);
            if (mountainPlace != null)
            {
                return new NewMountainPlaceVm
                {
                    Id = mountainPlace.Id,
                    Name = mountainPlace.Name,
                    Description = mountainPlace.Description,
                    CountryId = mountainPlace.CountryId,
                };
            }

            return null;
        }

        // Dodaj nowe miejsce górskie
        public int AddMountainPlace(NewMountainPlaceVm model)
        {
            // Mapowanie ViewModel do modelu domenowego
            var mountainPlace = _mapper.Map<MountainPlace>(model);

            // Jeśli jest zdjęcie, zapisujemy go jako tablicę bajtów
          
            // Dodajemy do bazy danych poprzez repository
            return _mountainPlaceRepository.Add(mountainPlace);
        }

        // Aktualizuj miejsce górskie
        public void UpdateMountainPlace(NewMountainPlaceVm model)
        {
            var mountainPlace = _mountainPlaceRepository.GetById(model.Id);
            if (mountainPlace != null)
            {
                mountainPlace.Name = model.Name;
                mountainPlace.Description = model.Description;
                mountainPlace.CountryId = model.CountryId;

                _mountainPlaceRepository.Update(mountainPlace);  // Aktualizujemy w repozytorium
            }
        }

        // Usuń miejsce górskie
        public void DeleteMountainPlace(int id)
        {
            var mountainPlace = _mountainPlaceRepository.GetById(id);
            if (mountainPlace != null)
            {
                _mountainPlaceRepository.Delete(id);  // Usuwamy z repozytorium
            }
        }

        // Pobierz wszystkie miejsca górskie powiązane z danym krajem
        public ListMountainPlaceVm GetAllMountainPlacesForCountry(int countryId)
        {
            var places = _mountainPlaceRepository.GetByCountryId(countryId).ToList();  // Pobieramy miejsca powiązane z krajem

            var model = new ListMountainPlaceVm
            {
                MountainPlaces = places.Select(mp => new MountainPlaceForListVm
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    CountryId = mp.CountryId,
                }).ToList()
            };

            return model;
        }

        // Pobierz wszystkie miejsca górskie
        public List<MountainPlace> GetAllMountainPlaces()
        {
            return _mountainPlaceRepository.GetAll().ToList();  // Zwracamy wszystkie miejsca
        }

        // Pobierz miejsce górskie na podstawie jego ID
        public MountainPlace GetMountainPlaceById(int mountainPlaceId)
        {
            return _mountainPlaceRepository.GetById(mountainPlaceId);  // Zwracamy miejsce po ID
        }
        public List<CountryForListVm> GetAllCountries()
        {
            var countries = _countryRepository.GetAllCountries(); // Pobieramy kraje z repozytorium
            var countryVms = _mapper.Map<List<CountryForListVm>>(countries); // Mapowanie na ViewModel (jeśli używasz AutoMapper)

            return countryVms;
        }
        // Serwis
        public ListMountainPlaceVm GetMountainPlacesByCountryId(int countryId)
        {
            // Pobieramy miejsca powiązane z krajem
            var places = _mountainPlaceRepository.GetByCountryId(countryId).ToList();

            // Mapowanie danych na ViewModel (ListMountainPlaceVm)
            var model = new ListMountainPlaceVm
            {
                MountainPlaces = places.Select(mp => new MountainPlaceForListVm
                {
                    Id = mp.Id,
                    Name = mp.Name,
                    Description = mp.Description,
                    CountryId = mp.CountryId,
                }).ToList()
            };

            return model;
        }

    }
}
