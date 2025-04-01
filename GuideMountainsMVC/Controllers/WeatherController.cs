using GuideMountainsMVC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Json(new { success = false, error = "Proszę wpisać miasto." });
            }

            var weatherResponse = await _weatherService.GetWeatherAsync(city);

            if (weatherResponse == null)
            {
                return Json(new { success = false, error = "Nie udało się pobrać danych pogodowych." });
            }

            return Json(new
            {
                success = true,
                weatherData = new
                {
                    cityName = weatherResponse.Name,
                    weatherDescription = weatherResponse.Weather?[0]?.Description ?? "Brak danych",
                    temperature = weatherResponse.Main?.Temp ?? 0
                },
                snowData = new
                {
                    the1h = weatherResponse.Snow?.The1h ?? 0,
                    the3h = weatherResponse.Snow?.The3h ?? 0,
                    the24h = weatherResponse.Snow?.The24h ?? 0
                }
            });
        }

        // 🔥 Nowa metoda: Pobiera 5-dniową prognozę pogody
        [HttpPost]
        public async Task<IActionResult> GetFiveDayForecast(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                return Json(new { success = false, error = "Proszę wpisać miasto." });
            }

            try
            {
                var forecastResponse = await _weatherService.GetFiveDayForecastAsync(city);

                if (forecastResponse == null || forecastResponse.Forecasts == null || forecastResponse.Forecasts.Count == 0)
                {
                    Console.WriteLine($"[ERROR] Brak prognozy dla miasta: {city}");
                    return Json(new { success = false, error = "Nie udało się pobrać prognozy pogody." });
                }

                return Json(new
                {
                    success = true,
                    city = forecastResponse.CityName,
                    forecast = forecastResponse.Forecasts.Select(f => new
                    {
                        date = f.Date.ToString("yyyy-MM-dd"),
                        minTemp = f.MinTemp,
                        maxTemp = f.MaxTemp,
                        weatherDescription = f.WeatherDescription,
                        icon = $"https://openweathermap.org/img/w/{f.Icon}.png"
                    })
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION] Błąd podczas pobierania prognozy: {ex.Message}");
                return Json(new { success = false, error = "Wystąpił błąd serwera podczas pobierania prognozy pogody." });
            }
        }

    }
}
