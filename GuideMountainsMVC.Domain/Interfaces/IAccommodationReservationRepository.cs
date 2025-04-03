using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface IAccommodationReservationRepository
    {
        void AddReservation(AccommodationReservation reservation);
        bool IsAccommodationAvailable(int accommodationId, DateTime start, DateTime end);
        List<AccommodationReservation> GetReservationsForAccommodation(int accommodationId);
    }
}
