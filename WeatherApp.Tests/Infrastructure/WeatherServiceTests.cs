using System.Threading.Tasks;
using Moq;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Domain;
using WeatherApp.Infrastructure;
using Xunit;

namespace WeatherApp.Tests.Infrastructure
{
    public class WeatherServiceTests
    {
        [Fact]
        public async Task GetWeatherAsync_ShouldReturnCityWeather()
        {
            // Arrange
            string cityName = "London";
            WeatherService weatherService = new WeatherService();

            // Act
            var result = await weatherService.GetWeatherAsync(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.City);
            Assert.NotNull(result.Temperature);
            Assert.NotNull(result.Condition);
        }
    }
}
