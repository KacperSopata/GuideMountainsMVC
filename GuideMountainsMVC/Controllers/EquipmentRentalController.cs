using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Controllers
{
    public class EquipmentRentalController : Controller
    {
        private readonly IEquipmentRentalService _equipmentRentalService;
        private readonly ICountryService _countryService;
        private readonly IMountainPlaceService _mountainPlaceService;

        public EquipmentRentalController(
            IEquipmentRentalService equipmentRentalService,
            ICountryService countryService,
            IMountainPlaceService mountainPlaceService)
        {
            _equipmentRentalService = equipmentRentalService;
            _countryService = countryService;
            _mountainPlaceService = mountainPlaceService;
        }

        // Widok wszystkich krajów
        [HttpGet]
        public IActionResult Index()
        {
            var countries = _countryService.GetAllCountries();
            var model = countries.Select(c => new Country
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return View(model);
        }

        // Widok miejsc górskich dla danego kraju
        [HttpGet]
        public IActionResult MountainPlacesForEquipmentRental(int countryId)
        {
            var placesVm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            ViewBag.CountryId = countryId;
            ViewBag.CountryName = _countryService.GetCountryById(countryId)?.Name;
            return View("MountainPlacesForEquipmentRental", placesVm);
        }

        // GET: formularz dodawania
        [HttpGet]
        public IActionResult AddEquipmentRental(int? countryId = null, int? mountainPlaceId = null)
        {
            var countries = _countryService.GetAllCountries();
            var categories = _equipmentRentalService.GetAllCategories();

            var model = new NewEquipmentRentalVm
            {
                CountryId = countryId ?? 0,
                MountainPlaceId = mountainPlaceId ?? 0,
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                MountainPlaces = _mountainPlaceService
                    .GetMountainPlacesByCountryId(countryId ?? 0)
                    .MountainPlaces.Select(mp => new SelectListItem
                    {
                        Value = mp.Id.ToString(),
                        Text = mp.Name
                    }).ToList(),
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        // POST: dodanie wypożyczenia
        [HttpPost]
        public async Task<IActionResult> AddEquipmentRental(NewEquipmentRentalVm model)
        {
            if (model.ImageContent != null && model.ImageContent.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.ImageContent.CopyToAsync(ms);
                    model.Image = ms.ToArray();
                }
            }

            _equipmentRentalService.AddEquipmentRental(model);
            return RedirectToAction("Index");
        }

        // AJAX: pobierz miejsca górskie na podstawie kraju
        [HttpGet]
        public JsonResult GetMountainPlacesByCountryId(int countryId)
        {
            var result = _mountainPlaceService
                .GetMountainPlacesByCountryId(countryId)
                .MountainPlaces.Select(mp => new
                {
                    Value = mp.Id,
                    Text = mp.Name
                }).ToList();

            return Json(result);
        }

        [HttpGet]
        public IActionResult EquipmentRentalDetails(int id)
        {
            var model = _equipmentRentalService.GetEquipmentRentalDetail(id);
            return View(model);
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _equipmentRentalService.DeleteEquipmentRental(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _equipmentRentalService.DeleteEquipmentRental(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EquipmentRentalsForMountainPlace(int mountainPlaceId)
        {
            var list = _equipmentRentalService.GetEquipmentRentalsForMountainPlace(mountainPlaceId);

            var model = new ListEquipmentRentalVm
            {
                EquipmentRentals = list // zakładam że zwraca List<EquipmentRentalForListVm>
            };

            var place = _mountainPlaceService.GetMountainPlaceById(mountainPlaceId);
            ViewBag.MountainPlaceName = place?.Name;
            ViewBag.MountainPlaceId = mountainPlaceId;

            return View(model);
        }
    }
}
