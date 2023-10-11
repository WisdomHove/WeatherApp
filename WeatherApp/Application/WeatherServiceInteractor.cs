using System.Threading.Tasks;
using WeatherApp.Core;

namespace WeatherApp.Application
{
    public class WeatherServiceInteractor
    {
        private readonly IWeatherService _weatherService;

        public WeatherServiceInteractor(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<CityWeather> GetWeatherAsync(string cityName)
        {
            return await _weatherService.GetWeatherAsync(cityName);
        }
    }
}

