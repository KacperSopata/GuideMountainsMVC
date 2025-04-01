using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;

namespace GuideMountainsMVC.Application.Services
{
    public class EquipmentRentalService : IEquipmentRentalService
    {
        private readonly IEquipmentRentalRepository _equipmentRentalRepository;
        private readonly IMapper _mapper;

        public EquipmentRentalService(IEquipmentRentalRepository equipmentRentalRepository, IMapper mapper)
        {
            _equipmentRentalRepository = equipmentRentalRepository;
            _mapper = mapper;
        }

        public ListEquipmentRentalVm GetAllEquipmentRentals(int pageSize, int currentPage, string searchString)
        {
            var query = _equipmentRentalRepository.GetAllEquipmentRentals();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(e => e.Name.Contains(searchString) || e.Description.Contains(searchString));
            }

            var totalCount = query.Count();
            var equipmentRentals = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new ListEquipmentRentalVm
            {
                EquipmentRentals = _mapper.Map<List<EquipmentRentalForListVm>>(equipmentRentals),
                CurrentPage = currentPage,
                PageSize = pageSize,
                Count = totalCount
            };
        }

        public EquipmentRentalDetailVm GetEquipmentRentalDetail(int id)
        {
            var equipmentRental = _equipmentRentalRepository.GetEquipmentRentalById(id);
            return _mapper.Map<EquipmentRentalDetailVm>(equipmentRental);
        }

        public int AddEquipmentRental(NewEquipmentRentalVm newEquipmentRental)
        {
            var equipmentRental = _mapper.Map<EquipmentRental>(newEquipmentRental);
            if (newEquipmentRental.ImageContent != null)
            {
                equipmentRental.Image = newEquipmentRental.Image;
            }
            _equipmentRentalRepository.AddEquipmentRental(equipmentRental);
            return equipmentRental.Id;
        }

        public void UpdateEquipmentRental(NewEquipmentRentalVm updatedEquipmentRental)
        {
            var equipmentRental = _mapper.Map<EquipmentRental>(updatedEquipmentRental);
            _equipmentRentalRepository.UpdateEquipmentRental(equipmentRental);
        }

        public void DeleteEquipmentRental(int id)
        {
            var equipmentRental = _equipmentRentalRepository.GetEquipmentRentalById(id);
            if (equipmentRental != null)
            {
                _equipmentRentalRepository.DeleteEquipmentRental(equipmentRental);
            }
        }

        public ListEquipmentRentalVm GetEquipmentRentalsByCountryId(int countryId)
        {
            var equipmentRentals = _equipmentRentalRepository.GetByCountryId(countryId).ToList();

            return new ListEquipmentRentalVm
            {
                EquipmentRentals = equipmentRentals.Select(er => _mapper.Map<EquipmentRentalForListVm>(er)).ToList()
            };
        }

        public ListEquipmentRentalVm GetEquipmentRentalsByMountainPlaceId(int mountainPlaceId)
        {
            var equipmentRentals = _equipmentRentalRepository.GetEquipmentRentalsByMountainPlaceId(mountainPlaceId);

            return new ListEquipmentRentalVm
            {
                EquipmentRentals = _mapper.Map<List<EquipmentRentalForListVm>>(equipmentRentals)
            };
        }

        public List<CategoryEquipmentRental> GetAllCategories()
        {
            return _equipmentRentalRepository.GetAllCategories().ToList();
        }

        public EquipmentRentalBasicVm GetEquipmentRentalById(int id)
        {
            var rental = _equipmentRentalRepository.GetById(id);
            return _mapper.Map<EquipmentRentalBasicVm>(rental);
        }

        public List<MountainPlaceForListVm> GetMountainPlacesForCountry(int countryId)
        {
            var places = _equipmentRentalRepository.GetMountainPlacesByCountryId(countryId);
            return _mapper.Map<List<MountainPlaceForListVm>>(places);
        }
        public List<EquipmentRentalForListVm> GetEquipmentRentalsForMountainPlace(int mountainPlaceId)
        {
            var rentals = _equipmentRentalRepository.GetEquipmentRentalsByMountainPlaceId(mountainPlaceId);
            return _mapper.Map<List<EquipmentRentalForListVm>>(rentals);
        }

        public ListEquipmentRentalVm GetEquipmentRentalsByCategoryId(int categoryForeignKey)
        {
            var equipmentRentals = _equipmentRentalRepository.GetByCategoryId(categoryForeignKey).ToList();

            var vm = new ListEquipmentRentalVm
            {
                EquipmentRentals = _mapper.Map<List<EquipmentRentalForListVm>>(equipmentRentals)
            };

            return vm;
        }

    }
}
