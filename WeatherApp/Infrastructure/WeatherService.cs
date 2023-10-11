using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Core;

namespace WeatherApp.Infrastructure
{
    public class WeatherService : IWeatherService
    {
        private const string WeatherStackApiKey = "74878fe556a8f8f31fb79452929893a2"; 

        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CityWeather> GetWeatherAsync(string cityName)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"http://api.weatherstack.com/current?access_key={WeatherStackApiKey}&query={cityName}");
                response.EnsureSuccessStatusCode(); // Ensure a successful API call

                string responseData = await response.Content.ReadAsStringAsync();
                dynamic weatherData = JsonConvert.DeserializeObject(responseData);

                var cityWeather = new CityWeather
                {
                    City = cityName,
                    Temperature = $"{weatherData.current.temperature} °C",
                    Condition = weatherData.current.weather_descriptions[0]
                };

                return cityWeather;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}

