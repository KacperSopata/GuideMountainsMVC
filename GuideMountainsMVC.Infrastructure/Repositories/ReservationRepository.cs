using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GuideMountainsMVC.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public ReservationRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation); 
            _context.SaveChanges();
        }


        public Reservation GetReservationById(int id)
        {
            return _context.Reservations
                .Include(r => r.ReservationItems)
                    .ThenInclude(ri => ri.SkiPass)
                .Include(r => r.ReservationItems)
                    .ThenInclude(ri => ri.Accommodation)
                .Include(r => r.ReservationItems)
                    .ThenInclude(ri => ri.EquipmentRental)
                .FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetUserReservations()
        {
            return _context.Reservations
                .Include(r => r.ReservationItems)
                .ToList();
        }

        public void UpdateReservation(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            _context.SaveChanges();
        }

        public void RemoveItem(int reservationId, int itemId)
        {
            var reservation = _context.Reservations
                .Include(r => r.ReservationItems)
                .FirstOrDefault(r => r.Id == reservationId);

            if (reservation != null)
            {
                var item = reservation.ReservationItems.FirstOrDefault(ri => ri.Id == itemId);
                if (item != null)
                {
                    reservation.ReservationItems.Remove(item);
                    _context.SaveChanges();
                }
            }
        }

        public void AddItemToReservation(int reservationId, ReservationItem item)
        {
            var reservation = _context.Reservations.Include(r => r.ReservationItems).FirstOrDefault(r => r.Id == reservationId);
            if (reservation != null)
            {
                reservation.ReservationItems.Add(item);
                _context.SaveChanges();
            }
        }

        public ReservationItem GetReservationItemById(int itemId)
        {
            return _context.ReservationItems
                .Include(ri => ri.SkiPass)
                .Include(ri => ri.Accommodation)
                .Include(ri => ri.EquipmentRental)
                .FirstOrDefault(ri => ri.Id == itemId);
        }
        public MountainPlace GetMountainPlaceById(int id)
        {
            return _context.MountainPlaces.FirstOrDefault(mp => mp.Id == id);
        }
        public EquipmentRental GetEquipmentRentalById(int id)
        {
            return _context.EquipmentRentals.FirstOrDefault(e => e.Id == id);
        }

        public SkiPassType GetSkiPassTypeById(int id)
        {
            return _context.SkiPassTypes.FirstOrDefault(s => s.Id == id);
        }
        public IQueryable<Reservation> GetAll()
        {
            return _context.Reservations
                .Include(r => r.ReservationItems)
                .Include(r => r.User);
        }

        public void DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

    }
}
