using Xunit;
using Moq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Domain.Model;
using Moq.Protected;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using GuideMountainsMVC.Domain.Model.GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Tests
{
    public class WeatherServiceTests
    {
        private WeatherService CreateService(string apiResponse, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(apiResponse, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(handlerMock.Object);
            return new WeatherService(httpClient, "dummy-api-key");
        }

        [Fact]
        public async Task GetWeatherAsync_ValidResponse_ReturnsWeather()
        {
            var fakeResponse = JsonConvert.SerializeObject(new WeatherResponse { Name = "TestCity" });
            var service = CreateService(fakeResponse);

            var result = await service.GetWeatherAsync("TestCity");

            Assert.NotNull(result);
            Assert.Equal("TestCity", result.Name);
        }

        [Fact]
        public async Task GetWeatherAsync_InvalidStatusCode_ReturnsNull()
        {
            var service = CreateService("Not Found", HttpStatusCode.NotFound);

            var result = await service.GetWeatherAsync("InvalidCity");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetFiveDayForecastAsync_ValidResponse_ReturnsForecast()
        {
            // 🔧 Sztuczna odpowiedź JSON jak z API
            var fakeJson = @"{
        ""list"": [
            {
                ""dt"": 1711987200,
                ""main"": { ""temp"": 10 },
                ""weather"": [ { ""description"": ""sunny"", ""icon"": ""01d"" } ]
            },
            {
                ""dt"": 1711987200,
                ""main"": { ""temp"": 15 },
                ""weather"": [ { ""description"": ""sunny"", ""icon"": ""01d"" } ]
            }
        ]
    }";

            var service = CreateService(fakeJson); // używa HttpClient z mockowaną odpowiedzią

            var result = await service.GetFiveDayForecastAsync("TestCity");

            Assert.NotNull(result);
            Assert.Equal("TestCity", result.CityName);
            Assert.NotEmpty(result.Forecasts);
        }


        [Fact]
        public async Task GetFiveDayForecastAsync_EmptyList_ReturnsNull()
        {
            var forecastList = new WeatherForecastResponse { List = null };
            var fakeResponse = JsonConvert.SerializeObject(forecastList);

            var service = CreateService(fakeResponse);
            var result = await service.GetFiveDayForecastAsync("TestCity");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetFiveDayForecastAsync_ApiError_ReturnsNull()
        {
            var service = CreateService("Error", HttpStatusCode.InternalServerError);
            var result = await service.GetFiveDayForecastAsync("TestCity");
            Assert.Null(result);
        }
    }
}
