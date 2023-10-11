using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public interface IWeatherService
    {
        Task<CityWeather> GetWeatherAsync(string cityName);
    }
}

