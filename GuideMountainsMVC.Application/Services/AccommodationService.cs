using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;

namespace GuideMountainsMVC.Application.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IMapper _mapper;

        public AccommodationService(IAccommodationRepository accommodationRepository, IMapper mapper)
        {
            _accommodationRepository = accommodationRepository;
            _mapper = mapper;
        }

        public ListAccommodationVm GetAllAccommodations(int pageSize, int currentPage, string searchString)
        {
            var query = _accommodationRepository.GetAllAccommodations();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.Name.Contains(searchString));
            }

            var totalCount = query.Count();
            var accommodations = query.Skip((currentPage - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToList();

            var viewModel = new ListAccommodationVm
            {
                Accommodations = _mapper.Map<List<AccommodationForListVm>>(accommodations),
                CurrentPage = currentPage,
                PageSize = pageSize,
                SearchString = searchString,
                Count = totalCount
            };

            return viewModel;
        }

        public AccommodationDetailVm GetAccommodationDetail(int id)
        {
            var accommodation = _accommodationRepository.GetAccommodationById(id);
            return _mapper.Map<AccommodationDetailVm>(accommodation);
        }

        public int AddAccommodation(NewAccommodationVm newAccommodation)
        {
            var accommodation = _mapper.Map<Accommodation>(newAccommodation);
            if (newAccommodation.ImageContent != null)
            {
                accommodation.Image = newAccommodation.Image;
            }
            _accommodationRepository.AddAccommodation(accommodation);
            return accommodation.Id;
        }

        public void UpdateAccommodation(NewAccommodationVm updatedAccommodation)
        {
            var accommodation = _mapper.Map<Accommodation>(updatedAccommodation);
            _accommodationRepository.UpdateAccommodation(accommodation);
        }

        public void DeleteAccommodation(int id)
        {
            var accommodation = _accommodationRepository.GetAccommodationById(id);
            if (accommodation != null)
            {
                _accommodationRepository.DeleteAccommodation(accommodation);
            }
        }

        public ListAccommodationVm GetAccommodationByCountryId(int countryId)
        {
            var places = _accommodationRepository.GetByCountryId(countryId).ToList();

            // Mapowanie danych na ViewModel (ListMountainPlaceVm)
            var model = new ListAccommodationVm
            {
                Accommodations = places.Select(mp => new AccommodationForListVm
                {
                    Id = mp.Id,
                    Description = mp.Description,
                    CountryId = mp.CountryId,
                }).ToList()
            };

            return model;
        }
        public ListAccommodationVm GetAccommodationsByMountainPlaceId(int mountainPlaceId)
        {
            var accommodations = _accommodationRepository.GetAccommodationsByMountainPlaceId(mountainPlaceId);

            var model = new ListAccommodationVm
            {
                Accommodations = _mapper.Map<List<AccommodationForListVm>>(accommodations)
            };

            return model;
        }
    }
}
