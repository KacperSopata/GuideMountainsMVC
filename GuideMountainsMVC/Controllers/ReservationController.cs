using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.ReservationVm;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace GuideMountainsMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ICountryService _countryService;
        private readonly IMountainPlaceService _mountainPlaceService;
        private readonly ISkiPassService _skiPassService;
        private readonly IAccommodationService _accommodationService;
        private readonly IEquipmentRentalService _equipmentService;
        private readonly IPriceCalculatorService _priceCalculatorService;

        public ReservationController(
            IReservationService reservationService,
            ICountryService countryService,
            IMountainPlaceService mountainPlaceService,
            ISkiPassService skiPassService,
            IAccommodationService accommodationService,
            IEquipmentRentalService equipmentService,
            IPriceCalculatorService priceCalculatorService)
        {
            _reservationService = reservationService;
            _countryService = countryService;
            _mountainPlaceService = mountainPlaceService;
            _skiPassService = skiPassService;
            _accommodationService = accommodationService;
            _equipmentService = equipmentService;
            _priceCalculatorService = priceCalculatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var countriesVm = _countryService.GetAllCountries();
            var model = countriesVm.Select(c => new Country
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult MountainPlacesForCountry(int countryId)
        {
            var placesVm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);

            ViewBag.CountryId = countryId;
            ViewBag.CountryName = _countryService.GetCountryById(countryId)?.Name;

            return View(placesVm); // ✅ To ma być ListMountainPlaceVm
        }


        [HttpGet]
        public IActionResult SelectItemsForReservation(int countryId, int mountainPlaceId)
        {
            var skiPasses = _skiPassService.GetSkiPassByCountryId(countryId).SkiPasses;
            var skiPassTypes = _skiPassService.GetAllSkiPassTypes();

            var model = new NewReservationVm
            {
                CountryId = countryId,
                MountainPlaceId = mountainPlaceId,

                AvailableSkiPasses = skiPasses.Select(sp => new SelectListItem
                {
                    Value = sp.Id.ToString(),
                    Text = sp.MountainPlaceName
                }).ToList(),

                AvailableSkiPassTypes = skiPassTypes.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList(),

                AvailableAccommodations = _accommodationService
                        .GetAccommodationsByMountainPlaceId(mountainPlaceId)
                        .Accommodations
                        .Select(acc => new SelectListItem
                        {
                            Value = acc.Id.ToString(),
                            Text = acc.Name
                        }).ToList(),

                AvailableEquipment = _equipmentService.GetEquipmentRentalsByMountainPlaceId(mountainPlaceId)
                    .EquipmentRentals.Select(eq => new SelectListItem
                    {
                        Value = eq.Id.ToString(),
                        Text = eq.Name
                    }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateReservation(NewReservationVm reservationVm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            reservationVm.UserId = userId;

            var mountainPlace = _mountainPlaceService.GetMountainPlaceById(reservationVm.MountainPlaceId);
            reservationVm.MountainPlaceName = mountainPlace?.Name ?? "Unknown";

            int reservationId = _reservationService.CreateReservation(reservationVm);

            // --- SKIPASS ---
            if (reservationVm.SkiPassSelections != null && reservationVm.SkiPassSelections.Any())
            {
                foreach (var selection in reservationVm.SkiPassSelections)
                {
                    if (selection.SkiPassId != 0 && selection.SkiPassTypeId != 0 && selection.Days > 0 && selection.Quantity > 0)
                    {
                        var skiPass = _skiPassService.GetSkiPassById(selection.SkiPassId);
                        var skiPassType = _skiPassService.GetSkiPassTypeById(selection.SkiPassTypeId);

                        if (skiPass == null || skiPassType == null)
                        {
                            throw new Exception("Nie znaleziono danych SkiPass lub typu.");
                        }

                        double unitPrice = _priceCalculatorService.CalculateSkiPassPrice(skiPass.Id, skiPassType.Id, selection.Days);
                        double totalPrice = unitPrice * selection.Quantity;

                        var item = new ReservationItemVm
                        {
                            SkiPassId = skiPass.Id,
                            SkiPassName = skiPass.Description,
                            SkiPassTypeName = skiPassType.Name,
                            SkiPassDays = selection.Days,
                            SkiPassQuantity = selection.Quantity,
                            Price = totalPrice
                        };

                        _reservationService.AddItemToReservation(reservationId, item);
                    }
                }
            }

            // --- ACCOMMODATION ---
            if (reservationVm.AccommodationSelections != null && reservationVm.AccommodationSelections.Any())
            {
                foreach (var acc in reservationVm.AccommodationSelections)
                {
                    if (acc.AccommodationId != 0 && acc.StartDate < acc.EndDate && acc.Guests > 0)
                    {
                        double accPrice = _priceCalculatorService.CalculateAccommodationPrice(
                            acc.AccommodationId, acc.StartDate, acc.EndDate, acc.Guests);

                        var accommodation = _accommodationService.GetAccommodationDetail(acc.AccommodationId);

                        var accItem = new ReservationItemVm
                        {
                            AccommodationId = acc.AccommodationId,
                            AccommodationName = accommodation.Name,
                            AccommodationGuests = acc.Guests,
                            AccommodationStartDate = acc.StartDate,
                            AccommodationEndDate = acc.EndDate,
                            AccommodationNights = (acc.EndDate - acc.StartDate).Days,
                            Price = accPrice
                        };

                        _reservationService.AddItemToReservation(reservationId, accItem);
                    }
                }
            }

            // --- EQUIPMENT ---
            if (reservationVm.EquipmentSelections != null && reservationVm.EquipmentSelections.Any())
            {
                foreach (var eq in reservationVm.EquipmentSelections)
                {
                    if (eq.EquipmentRentalId != 0 && eq.Days > 0 && eq.Quantity > 0)
                    {
                        var equipment = _equipmentService.GetEquipmentRentalById(eq.EquipmentRentalId);
                        if (equipment == null)
                        {
                            throw new Exception($"Nie znaleziono sprzętu o ID: {eq.EquipmentRentalId}");
                        }

                        double unitPrice = _priceCalculatorService.CalculateEquipmentPrice(eq.EquipmentRentalId, eq.Days);
                        double totalPrice = unitPrice * eq.Quantity;

                        var item = new ReservationItemVm
                        {
                            EquipmentRentalId = eq.EquipmentRentalId,
                            EquipmentName = equipment.Name,
                            EquipmentDays = eq.Days,
                            EquipmentQuantity = eq.Quantity,
                            Price = totalPrice
                        };

                        _reservationService.AddItemToReservation(reservationId, item);
                    }
                }
            }

            return RedirectToAction("Details", new { id = reservationId });
        }


        [HttpPost]
        public IActionResult AddItemToReservation(int reservationId, ReservationItemVm item)
        {
            _reservationService.AddItemToReservation(reservationId, item);
            return RedirectToAction("Details", new { id = reservationId });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var reservation = _reservationService.GetReservationDetails(id);
            return View(reservation);
        }

        [HttpPost]
        public IActionResult RemoveItem(int reservationId, int itemId)
        {
            _reservationService.RemoveItemFromReservation(reservationId, itemId);
            return RedirectToAction("Details", new { id = reservationId });
        }

        [HttpPost]
        public IActionResult ConfirmReservation(int reservationId)
        {
            _reservationService.ConfirmReservation(reservationId);
            return RedirectToAction("MyReservations");
        }


        [HttpGet]
        public IActionResult MyReservations()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reservations = _reservationService.GetReservationsForUser(userId);
            return View(reservations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _reservationService.DeleteReservation(id);
            return RedirectToAction("MyReservations");
        }

    }
}
