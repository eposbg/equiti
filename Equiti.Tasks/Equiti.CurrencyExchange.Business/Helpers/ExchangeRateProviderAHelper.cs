using Equiti.CurrencyExchange.Business.ProviderA;
using Newtonsoft.Json;

namespace Equiti.CurrencyExchange.Business.Helpers
{
    public class ExchangeRateProviderAHelper : ExchangeRateProviderHelperBase, IExchangeRateProviderHelper
    {
        public ExchangeRateProviderAHelper(string json) : base(json)
        {
        }

        public override IEnumerable<string> GetCurrencyPairs()
        {
            List<ProviderAData>? data = JsonConvert.DeserializeObject<List<ProviderAData>>(base.Json);
            if (data==null)
            {
                throw new ArgumentException($"Cannot convert {base.Json} to List<ProviderAData> ");
            }
            return data.Select(x => x.CurrencyPair).Distinct();
        }

        public override decimal? GetRateForDate(string currencyPair, DateTime day)
        {
            var data = JsonConvert.DeserializeObject<List<ProviderAData>>(base.Json);
            var pairs =  data?
                .Where(x => string.Equals(x.CurrencyPair, currencyPair, StringComparison.CurrentCultureIgnoreCase))
                .Where(x => DateTime.Parse(x.Date) == day)
                .SingleOrDefault();

            return pairs?.Rate ?? null;
        }

        public static bool CanParse(string json)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<List<ProviderAData>>(json);
                return result!=null;
            }
            catch
            {
                return false;
            }
        }
    }
}
