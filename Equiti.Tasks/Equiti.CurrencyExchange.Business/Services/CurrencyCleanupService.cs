using Equiti.CurrencyExchange.Business.Helpers;

namespace Equiti.CurrencyExchange.Business.Services
{
    public class CurrencyCleanupService
    {
        private readonly IExchangeRateProviderHelper provider1Helper;
        private readonly IExchangeRateProviderHelper provider2Helper;

        public CurrencyCleanupService(string jsonFeed1, string jsonFeed2)
        {
            this.provider1Helper = new ExchangeRateProviderFactory().Create(jsonFeed1);
            this.provider2Helper = new ExchangeRateProviderFactory().Create(jsonFeed2);
        }

        public async Task<Dictionary<string, decimal>> GetLowestCurrencyRatesAsync(DateTime day)
        {
            
            var result = new Dictionary<string, decimal>();

            List<string> currencyPairs1 = provider1Helper.GetCurrencyPairs().ToList();
            List<string> currencyPairs2 = provider2Helper.GetCurrencyPairs().ToList();

            List<string> mergedCurrencies = currencyPairs1.Concat(currencyPairs2)
                .Distinct()
                .ToList();

            foreach (var currencyPair in mergedCurrencies)
            {
                var tasks = new List<Task<decimal?>>();
                tasks.Add(Task.Run<decimal?>(() => provider1Helper.GetRateForDate(currencyPair, day)));
                tasks.Add(Task.Run<decimal?>(() => provider2Helper.GetRateForDate(currencyPair, day)));

                await Task.WhenAll(tasks);
                decimal? lowestValue = GetLowestDecimal(tasks.Select(x => x.Result).ToArray());
                if (lowestValue.HasValue)
                {
                    result.Add(currencyPair, lowestValue.Value);
                }
            }

            return result;
        }

        private decimal? GetLowestDecimal(decimal?[] values)
        {
            decimal? result = null;
            foreach (var value in values)
            {
                if (!value.HasValue || value.Value == 0) { continue; }

                if (!result.HasValue)
                {
                    result = value;
                }
                else if (result > value)
                {
                    result = value;
                }
            }

            return result;
        }
    }
}
