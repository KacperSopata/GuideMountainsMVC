using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.ReservationVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace GuideMountainsMVC.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public void AddItemToReservation(int reservationId, ReservationItemVm itemVm)
        {
            var item = new ReservationItem
            {
                SkiPassId = itemVm.SkiPassId,
                SkiPassQuantity = itemVm.SkiPassQuantity,
                SkiPassDays = itemVm.SkiPassDays,
                SkiPassTypeName = itemVm.SkiPassTypeName,
                SkiPass = null, // opcjonalnie

                AccommodationId = itemVm.AccommodationId,
                AccommodationGuests = itemVm.AccommodationGuests,
                AccommodationNights = itemVm.AccommodationNights,
                AccommodationStartDate = itemVm.AccommodationStartDate,
                AccommodationEndDate = itemVm.AccommodationEndDate,


                EquipmentRentalId = itemVm.EquipmentRentalId,
                EquipmentQuantity = itemVm.EquipmentQuantity,
                EquipmentDays = itemVm.EquipmentDays,
                EquipmentName = itemVm.EquipmentName,
                //EquipmentRental = null,
                // ← WAŻNE

                Price = itemVm.Price
            };

            _reservationRepository.AddItemToReservation(reservationId, item);
        }

        public void ConfirmReservation(int reservationId)
        {
            var reservation = _reservationRepository.GetReservationById(reservationId);
            if (reservation != null)
            {
                reservation.IsConfirmed = true;
                _reservationRepository.UpdateReservation(reservation);
            }
        }

        public int CreateReservation(NewReservationVm reservationVm)
        {
            var mountainPlace = _reservationRepository.GetMountainPlaceById(reservationVm.MountainPlaceId);

            if (mountainPlace == null)
            {
                throw new Exception($"Nie znaleziono miejsca górskiego o ID: {reservationVm.MountainPlaceId}");
            }

            var mountainPlaceName = mountainPlace.Name;

            var reservation = new Reservation
            {
                CreatedDate = DateTime.Now,
                IsConfirmed = false,
                MountainPlaceName = mountainPlaceName,
                UserId = reservationVm.UserId // ⬅️ przypisanie użytkownika
            };

            if (string.IsNullOrWhiteSpace(reservation.MountainPlaceName))
            {
                throw new Exception("MountainPlaceName jest pusty lub null! Zatrzymuję zapis.");
            }

            _reservationRepository.AddReservation(reservation);
            return reservation.Id;
        }
        public ReservationVm GetReservationDetails(int id)
        {
            var reservation = _reservationRepository.GetReservationById(id);
            return _mapper.Map<ReservationVm>(reservation);
        }

        public List<ReservationVm> GetUserReservations()
        {
            var reservations = _reservationRepository.GetUserReservations();
            return _mapper.Map<List<ReservationVm>>(reservations);
        }

        public void RemoveItemFromReservation(int reservationId, int itemId)
        {
            _reservationRepository.RemoveItem(reservationId, itemId);
        }
        public List<ReservationForListVm> GetReservationsForUser(string userId)
        {
            return _reservationRepository
                .GetAll()
                .Where(r => r.UserId == userId)
                .ProjectTo<ReservationForListVm>(_mapper.ConfigurationProvider)
                .ToList();
        }
        public void DeleteReservation(int id)
        {
            var reservation = _reservationRepository.GetReservationById(id);
            if (reservation == null)
                throw new Exception("Reservation not found.");

            _reservationRepository.DeleteReservation(reservation);
        }


    }
}
