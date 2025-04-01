using Microsoft.AspNetCore.Mvc;
using GuideMountainsMVC.Application.Interfaces;


public class CountryController : Controller
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    // Akcja, która zwraca listę krajów
    public IActionResult Index()
    {
        // Pobieramy listę krajów z serwisu (repozytorium)
        var countries = _countryService.GetAllCountries();

        // Przekazujemy listę krajów do widoku
        return View(countries);
    }
}
