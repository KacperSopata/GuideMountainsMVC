using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.CountryVm;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuideMountainsMVC.Controllers
{
    public class MountainPlaceController : Controller
    {
        private readonly IMountainPlaceService _mountainPlaceService;
        private readonly ICountryService _countryService;

        public MountainPlaceController(IMountainPlaceService mountainPlaceService, ICountryService countryService)
        {
            _mountainPlaceService = mountainPlaceService;
            _countryService = countryService;
        }

        // Widok krajów
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

        [HttpGet]
        public IActionResult MountainPlacesForCountry(int countryId)
        {
            var mountainPlacesVm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);

            if (mountainPlacesVm == null || !mountainPlacesVm.MountainPlaces.Any())
            {
                ViewBag.Message = "No mountain places found for this country.";
            }

            var country = _countryService.GetCountryById(countryId);
            ViewBag.CountryName = country?.Name;
            ViewBag.CountryId = country?.Id;

            return View(mountainPlacesVm);
        }

        [HttpGet]
        public IActionResult AddMountainPlace()
        {
            var countries = _countryService.GetAllCountries();
            var model = new NewMountainPlaceVm
            {
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            ViewBag.CountryName = countries.FirstOrDefault()?.Name;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMountainPlace(NewMountainPlaceVm formModel)
        {
            Console.WriteLine($"ImageContent null? {formModel.ImageContent == null}");
            Console.WriteLine($"ImageContent Length: {formModel.ImageContent?.Length}");

            ModelState.Remove("Image");

            if (!ModelState.IsValid)
            {
                formModel.Countries = _countryService.GetAllCountries()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(formModel);
            }

            if (formModel.ImageContent != null && formModel.ImageContent.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await formModel.ImageContent.CopyToAsync(ms);
                    formModel.Image = ms.ToArray();
                }
            }
            else
            {
                ModelState.AddModelError("ImageContent", "Please upload a photo.");
                formModel.Countries = _countryService.GetAllCountries()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();
                return View(formModel);
            }

            _mountainPlaceService.AddMountainPlace(formModel);
            return RedirectToAction("Index");
        }

        public IActionResult MountainPlaceDetails(int id)
        {
            var model = _mountainPlaceService.GetMountainPlaceDetails(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _mountainPlaceService.DeleteMountainPlace(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _mountainPlaceService.DeleteMountainPlace(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult MountainPlacesForAccommodation(int countryId)
        {
            var vm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            var country = _countryService.GetCountryById(countryId);

            ViewBag.CountryId = countryId;
            ViewBag.CountryName = country?.Name;

            return View("MountainPlacesForAccommodation", vm);
        }

        [HttpGet]
        public IActionResult MountainPlacesForEquipmentRental(int countryId)
        {
            var vm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            var country = _countryService.GetCountryById(countryId);

            ViewBag.CountryId = countryId;
            ViewBag.CountryName = country?.Name;

            return View("MountainPlacesForEquipmentRental", vm);
        }
        [HttpGet]
        public IActionResult MountainPlacesForSkiPass(int countryId)
        {
            var vm = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            var country = _countryService.GetCountryById(countryId);

            ViewBag.CountryId = countryId;
            ViewBag.CountryName = country?.Name;

            return View("~/Views/MountainPlace/MountainPlacesForSkiPass.cshtml", vm);
        }



    }
}