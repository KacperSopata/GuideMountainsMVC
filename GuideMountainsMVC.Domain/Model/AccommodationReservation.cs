using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class AccommodationReservation
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; } // jeśli masz użytkowników, np. IdentityUser
    }
}
