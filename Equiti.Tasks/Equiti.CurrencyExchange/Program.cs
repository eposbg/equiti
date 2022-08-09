using Equiti.CurrencyExchange.Business.Services;
using System;

namespace Equiti.CurrencyExchange
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var mockResultProviderAJson = await File.ReadAllTextAsync("Data/ProviderAData.json");
            var mockResultProviderBJson = await File.ReadAllTextAsync("Data/ProviderBData.json");

            try
            {
                var currencyCleanupService = new CurrencyCleanupService(mockResultProviderAJson, mockResultProviderBJson);
                var date = new DateTime(2020, 10, 01);
                var result = await currencyCleanupService.GetLowestCurrencyRatesAsync(date);
                foreach (var currencyRate in result)
                {
                    Console.WriteLine($"\"{currencyRate.Key}\": {currencyRate.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}