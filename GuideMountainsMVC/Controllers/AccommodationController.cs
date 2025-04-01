using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Controllers
{
    public class AccommodationController : Controller
    {
        private readonly IAccommodationService _accommodationService;
        private readonly ICountryService _countryService;
        private readonly IMountainPlaceService _mountainPlaceService;

        public AccommodationController(
            IAccommodationService accommodationService,
            ICountryService countryService,
            IMountainPlaceService mountainPlaceService)
        {
            _accommodationService = accommodationService;
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
        public IActionResult MountainPlacesForCountry(int countryId)
        {
            var mountainPlacesVm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            var country = _countryService.GetCountryById(countryId);

            ViewBag.CountryName = country?.Name;
            ViewBag.CountryId = countryId;

            return View(mountainPlacesVm);
        }

        [HttpGet]
        public IActionResult MountainPlacesForAccommodation(int countryId)
        {
            var placesVm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            ViewBag.CountryId = countryId;
            ViewBag.CountryName = _countryService.GetCountryById(countryId)?.Name;
            return View("MountainPlacesForAccommodation", placesVm);
        }



        // GET: formularz dodawania
        [HttpGet]
        public IActionResult AddAccommodation(int? countryId = null, int? mountainPlaceId = null)
        {
            var countries = _countryService.GetAllCountries();

            var model = new NewAccommodationVm
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
                    }).ToList()
            };

            return View(model);
        }

        // POST: dodanie zakwaterowania
        [HttpPost]
        public async Task<IActionResult> AddAccommodation(NewAccommodationVm model)
        {
            if (model.ImageContent != null && model.ImageContent.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await model.ImageContent.CopyToAsync(ms);
                    model.Image = ms.ToArray();
                }
            }

            _accommodationService.AddAccommodation(model);
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
        public IActionResult AccommodationDetails(int id)
        {
            var model = _accommodationService.GetAccommodationDetail(id);
            return View(model);
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _accommodationService.DeleteAccommodation(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _accommodationService.DeleteAccommodation(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AccommodationsForMountainPlace(int mountainPlaceId)
        {
            var model = _accommodationService.GetAccommodationsByMountainPlaceId(mountainPlaceId); // zwraca już ListAccommodationVm

            var mountainPlace = _mountainPlaceService.GetMountainPlaceById(mountainPlaceId);
            ViewBag.MountainPlaceName = mountainPlace?.Name;
            ViewBag.MountainPlaceId = mountainPlaceId;

            return View("AccommodationsForMountainPlace", model);
        }
    }
}
