using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Domain.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Services
{
    public class ChatBotService : IChatBotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ChatBotService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<string> GetAnswerAsync(string userPrompt)
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                     new { role = "system", content = "Jesteś pomocnym asystentem."},
                     new { role = "user", content = userPrompt }
        },
                max_tokens = 100,
                temperature = 0.7
            };

            using var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
            var responseContent = await response.Content.ReadAsStringAsync(); // Odczytaj pełną odpowiedź
            Console.WriteLine($"OpenAI Response: {responseContent}"); // Wypisz w konsoli

            if (!response.IsSuccessStatusCode)
            {
                return $"Błąd API: {responseContent}";
            }

            var jsonResponse = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            if (jsonResponse == null || jsonResponse.Choices == null || jsonResponse.Choices.Count == 0)
            {
                return "Brak odpowiedzi";
            }


            var answer = jsonResponse.Choices.FirstOrDefault()?.Message?.Content?.Trim();
            return string.IsNullOrWhiteSpace(answer) ? $"Brak odpowiedzi - {responseContent}" : answer;
        }

    }
}
