using GuideMountainsMVC.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Model; // Dodaj referencję do modeli

namespace GuideMountainsMVC.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly List<WeatherHistory> _weatherHistory; // Przechowywanie historii w pamięci tymczasowej

        public WeatherService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey; // Przechowywanie klucza API
            _weatherHistory = new List<WeatherHistory>(); // Tymczasowe przechowywanie historii
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=pl";
            using var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responseContent);

            return weatherResponse;
        }
        public async Task SaveWeatherHistoryAsync(WeatherResponse weather, string city, string userId)
        {
            var weatherHistory = new WeatherHistory
            {
                City = city,
                Temperature = weather.Main.Temp,
                WeatherDescription = weather.Weather[0].Description,
                QueryDate = DateTime.UtcNow,
                UserId = userId
            };

            _weatherHistory.Add(weatherHistory); // Dodanie do pamięci
            Console.WriteLine($"Zapisano historię: {weatherHistory.City}, {weatherHistory.Temperature}°C, {weatherHistory.QueryDate}");

            await Task.CompletedTask;
        }

        public async Task<List<WeatherHistory>> GetWeatherHistoryAsync(string userId)
        {
            // Debugowanie - sprawdzenie zawartości listy historii
            Console.WriteLine($"Pobieranie historii dla użytkownika {userId}");
            var history = _weatherHistory.Where(x => x.UserId == userId).ToList();
            Console.WriteLine($"Znaleziono {history.Count} pozycji w historii dla {userId}");

            return await Task.FromResult(history); // Symulacja asynchronicznego zwrócenia historii
        }
    }
}
