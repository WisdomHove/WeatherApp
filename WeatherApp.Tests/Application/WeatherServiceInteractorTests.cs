using System.Threading.Tasks;
using Moq;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Domain;
using Xunit;

namespace WeatherApp.Tests.Application
{
    public class WeatherServiceInteractorTests
    {
        [Fact]
        public async Task GetWeatherAsync_ShouldCallWeatherServiceAndReturnCityWeather()
        {
            // Arrange
            string cityName = "London";
            CityWeather expectedCityWeather = new CityWeather
            {
                City = cityName,
                Temperature = "20 °C",
                Condition = "Sunny"
            };

            var mockWeatherService = new Mock<IWeatherService>();
            mockWeatherService.Setup(x => x.GetWeatherAsync(cityName))
                .ReturnsAsync(expectedCityWeather);

            var weatherInteractor = new WeatherServiceInteractor(mockWeatherService.Object);

            // Act
            var result = await weatherInteractor.GetWeatherAsync(cityName);

            // Assert
            Assert.Equal(expectedCityWeather, result);
        }
    }
}
