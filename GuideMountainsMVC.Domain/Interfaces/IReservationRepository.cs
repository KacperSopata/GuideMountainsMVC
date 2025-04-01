using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
        Reservation GetReservationById(int id);
        List<Reservation> GetUserReservations();
        void UpdateReservation(Reservation reservation);
        void RemoveItem(int reservationId, int itemId);

        // Operacje na elementach rezerwacji
        void AddItemToReservation(int reservationId, ReservationItem item);
        ReservationItem GetReservationItemById(int itemId);
        MountainPlace GetMountainPlaceById(int id);
        EquipmentRental GetEquipmentRentalById(int id);
        SkiPassType GetSkiPassTypeById(int id);
        IQueryable<Reservation> GetAll();
        void DeleteReservation(Reservation reservation);
    }
}
