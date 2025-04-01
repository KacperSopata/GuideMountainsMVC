using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Controllers
{
    public class SkiPassController : Controller
    {
        private readonly ISkiPassService _skiPassService;
        private readonly IMountainPlaceService _mountainPlaceService;
        private readonly ICountryService _countryService;

        public SkiPassController(ISkiPassService skiPassService, ICountryService countryService, IMountainPlaceService mountainPlaceService)
        {
            _skiPassService = skiPassService;
            _countryService = countryService;
            _mountainPlaceService = mountainPlaceService;
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
        [HttpGet]
        public IActionResult AddSkiPass()
        {
            var countries = _countryService.GetAllCountries();
            var SkiPassTypes = _skiPassService.GetAllSkiPassTypes();

            var model = new NewSkiPassVm
            {
                Countries = countries.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),

                MountainPlaces = new List<SelectListItem>(),
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddSkiPass(NewSkiPassVm model)
        {
            ModelState.Remove("Image");

            if (!ModelState.IsValid)
            {
                // tu dodaj do debugowania błędów ModelState:
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                errors.ForEach(e => Console.WriteLine($"ModelState Error: {e}"));

                model.Countries = _countryService.GetAllCountries()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();

               
                model.MountainPlaces = new List<SelectListItem>();

                return View(model);
            }

            if (model.ImageContent != null && model.ImageContent.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImageContent.CopyToAsync(memoryStream);
                    model.Image = memoryStream.ToArray();
                }
            }
            else
            {
                ModelState.AddModelError("", "Wybierz zdjęcie.");

                model.Countries = _countryService.GetAllCountries()
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList();

              

                model.MountainPlaces = new List<SelectListItem>();
                return View(model);
            }
            model.SkiPassTypeId = 1; // <- TU USTAWIAMY NA SZTYWNO "Normalny"
            var skiPassId = _skiPassService.AddSkiPass(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult SkiPassesForCountry(int countryId)
        {
            var skiPassVm = _skiPassService.GetSkiPassByCountryId(countryId);
            if (skiPassVm == null || !skiPassVm.SkiPasses.Any())
            {
                ViewBag.Message = "No Ski Pass found for this country.";
            }
            var country = _countryService.GetCountryById(countryId);
            ViewBag.CountryName = country?.Name;
            return View("SkiPassesForCountry", skiPassVm);
        }

       

        [HttpGet]
        public JsonResult GetMountainPlacesByCountryId(int countryId)
        {
            var mountainPlaces = _mountainPlaceService.GetMountainPlacesByCountryId(countryId);
            var result = mountainPlaces.MountainPlaces.Select(mp => new { Value = mp.Id, Text = mp.Name }).ToList();
            return Json(result);
        }

        public IActionResult SkiPassDetails(int id)
        {
            var model = _skiPassService.GetSkiPassDetail(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var skiPass = _skiPassService.GetSkiPassById(id);
            if (skiPass == null)
            {
                return NotFound();
            }

            // Perform the deletion logic
            _skiPassService.DeleteSkiPass(id);

            // Redirect to a list or confirmation page after deletion
            return RedirectToAction("Index");  // or wherever you want to send the user
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _skiPassService.DeleteSkiPass(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult SkiPassesForMountainPlace(int mountainPlaceId)
        {
            var model = _skiPassService.GetSkiPassesByMountainPlaceId(mountainPlaceId);
            var mountainPlace = _mountainPlaceService.GetMountainPlaceById(mountainPlaceId);

            ViewBag.MountainPlaceName = mountainPlace?.Name;
            ViewBag.MountainPlaceId = mountainPlaceId;

            return View("SkiPassesForMountainPlace", model);
        }


    }
}
