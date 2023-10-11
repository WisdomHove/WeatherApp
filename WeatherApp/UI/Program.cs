using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using WeatherApp.Application;
using WeatherApp.Core;
using WeatherApp.Domain;
using WeatherApp.Infrastructure;

namespace WeatherApp.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("WeatherApp - Get Current Weather");
                Console.WriteLine("Usage: WeatherApp <city_name>");
                Console.WriteLine("Example: WeatherApp London");
                return;
            }

            string cityName = args[0];

            IWeatherService weatherService = new WeatherService();
            WeatherServiceInteractor weatherInteractor = new WeatherServiceInteractor(weatherService);

            CityWeather cityWeather = await weatherInteractor.GetWeatherAsync(cityName);

            if (cityWeather != null)
            {
                Console.WriteLine($"Weather in {cityWeather.City}:");
                Console.WriteLine($"Temperature: {cityWeather.Temperature}");
                Console.WriteLine($"Condition: {cityWeather.Condition}");

                GeneratePdfReport(cityWeather);
            }
            else
            {
                Console.WriteLine("Failed to retrieve weather information.");
            }
        }

        private static void GeneratePdfReport(CityWeather cityWeather)
        {
            var pdfFileName = $"{cityWeather.City}_WeatherReport.pdf";

            // Create a new PdfWriter instance
            using (var writer = new PdfWriter(pdfFileName))
            {
                // Create a PdfDocument instance
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    var pdfModel = new PdfReportModel
                    {
                        City = cityWeather.City,
                        Temperature = cityWeather.Temperature,
                        Condition = cityWeather.Condition
                    };

                    // Create a paragraph for the content
                    var content = $"Weather in {pdfModel.City}:\n" +
                                  $"Temperature: {pdfModel.Temperature}\n" +
                                  $"Condition: {pdfModel.Condition}";

                    // Add the content to the document
                    document.Add(new Paragraph(content));
                }
            }

            Console.WriteLine($"PDF report generated: {pdfFileName}");
        }

    }
}

