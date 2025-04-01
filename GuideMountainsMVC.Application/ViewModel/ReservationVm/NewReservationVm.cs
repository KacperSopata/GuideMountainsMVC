using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;

namespace GuideMountainsMVC.Application.ViewModel.ReservationVm
{
    public class NewReservationVm
    {
        public int Id { get; set; }
        public int MountainPlaceId { get; set; }
        public int CountryId { get; set; }
        public string MountainPlaceName { get; set; }
        public string UserId { get; set; }

        // 🔸 Zamiast pojedynczych Selected... używamy kolekcji
        public List<SkiPassSelectionVm> SkiPassSelections { get; set; } = new();
        public List<EquipmentSelectionVm> EquipmentSelections { get; set; } = new();
        public List<AccommodationSelectionVm> AccommodationSelections { get; set; } = new();

        // 🔸 Lista wyborów do formularza
        public List<SelectListItem> Countries { get; set; } = new();
        public List<SelectListItem> MountainPlaces { get; set; } = new();
        public List<SelectListItem> AvailableSkiPasses { get; set; } = new();
        public List<SelectListItem> AvailableSkiPassTypes { get; set; } = new();
        public List<SelectListItem> AvailableAccommodations { get; set; } = new();
        public List<SelectListItem> AvailableEquipment { get; set; } = new();
    }

    public class AccommodationSelectionVm
    {
        public int AccommodationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Guests { get; set; }
    }
}
