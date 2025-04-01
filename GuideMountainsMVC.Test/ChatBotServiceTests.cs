using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.Services;
using Moq;
using Moq.Protected;
using Xunit;

namespace GuideMountainsMVC.Tests
{
    public class ChatBotServiceTests
    {
        private readonly string _apiKey = "fake-api-key";

        [Fact]
        public async Task GetAnswerAsync_ValidResponse_ReturnsAnswer()
        {
            // Arrange
            var expectedContent = @"{
                ""choices"": [
                    {
                        ""message"": {
                            ""content"": ""To jest testowa odpowiedź""
                        }
                    }
                ]
            }";

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedContent, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var service = new ChatBotService(httpClient, _apiKey);

            // Act
            var result = await service.GetAnswerAsync("Jak się masz?");

            // Assert
            Assert.Equal("To jest testowa odpowiedź", result);
        }

        [Fact]
        public async Task GetAnswerAsync_InvalidApiResponse_ReturnsErrorMessage()
        {
            // Arrange
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("Błąd API")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var service = new ChatBotService(httpClient, _apiKey);

            // Act
            var result = await service.GetAnswerAsync("Powiedz coś");

            // Assert
            Assert.Contains("Błąd API", result);
        }

        [Fact]
        public async Task GetAnswerAsync_EmptyChoices_ReturnsBrakOdpowiedzi()
        {
            // Arrange
            var emptyResponse = @"{ ""choices"": [] }";

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(emptyResponse, Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHandler.Object);
            var service = new ChatBotService(httpClient, _apiKey);

            // Act
            var result = await service.GetAnswerAsync("Cześć");

            // Assert
            Assert.Contains("Brak odpowiedzi", result);
        }
    }
}
