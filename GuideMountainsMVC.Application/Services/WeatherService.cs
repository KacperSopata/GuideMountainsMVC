using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric&lang=en";
                using var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherResponse>(responseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd w GetWeatherAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<WeatherFiveDayForecast> GetFiveDayForecastAsync(string city)
        {
            try
            {
                var url = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={_apiKey}&units=metric&lang=en";
                using var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Błąd API: {response.StatusCode}");
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Odpowiedź API:");
                Console.WriteLine(responseContent);

                var forecastResponse = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseContent);
                if (forecastResponse == null || forecastResponse.List == null)
                {
                    Console.WriteLine("Błąd: Nie udało się sparsować danych prognozy.");
                    return null;
                }

                var dailyForecasts = new Dictionary<DateTime, List<Forecast>>();

                foreach (var forecast in forecastResponse.List)
                {
                    var date = DateTimeOffset.FromUnixTimeSeconds(forecast.Dt).UtcDateTime.Date;
                    if (!dailyForecasts.ContainsKey(date))
                    {
                        dailyForecasts[date] = new List<Forecast>();
                    }
                    dailyForecasts[date].Add(forecast);
                }

                var fiveDayForecast = new WeatherFiveDayForecast
                {
                    CityName = city,
                    Forecasts = dailyForecasts.Select(entry => new DailyForecast
                    {
                        Date = entry.Key,
                        MinTemp = entry.Value.Min(f => f.Main.Temp),
                        MaxTemp = entry.Value.Max(f => f.Main.Temp),
                        WeatherDescription = entry.Value[0].Weather[0].Description,
                        Icon = entry.Value[0].Weather[0].Icon
                    }).ToList()
                };

                return fiveDayForecast;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd w GetFiveDayForecastAsync: {ex.Message}");
                return null;
            }
        }
    }
}
