using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsConfirmed { get; set; } = false;
        public string MountainPlaceName { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }
        //public ICollection<AccommodationReservation> AccommodationReservations { get; set; } = new List<AccommodationReservation>();
        public List<ReservationItem> ReservationItems { get; set; } = new List<ReservationItem>();
    }
}
