using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Model
{
    public class ReservationItem
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int? SkiPassId { get; set; }
        public SkiPass SkiPass { get; set; }
        public int? SkiPassDays { get; set; }
        public int? SkiPassQuantity { get; set; }
        public string SkiPassTypeName { get; set; }

        public int? AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }
        public DateTime? AccommodationStartDate { get; set; }
        public DateTime? AccommodationEndDate { get; set; }
        public int? AccommodationGuests { get; set; }
        public int? AccommodationNights { get; set; }

        public int? EquipmentRentalId { get; set; }
        public EquipmentRental EquipmentRental { get; set; }
        public int? EquipmentQuantity { get; set; }
        public int? EquipmentDays { get; set; }
        public string EquipmentName { get; set; }
        public string ItemType { get; set; }

        public double Price { get; set; }
    }

}
