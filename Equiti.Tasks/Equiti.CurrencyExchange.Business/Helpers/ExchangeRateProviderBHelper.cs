using Equiti.CurrencyExchange.Business.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Equiti.CurrencyExchange.Business.Helpers
{
    public class ExchangeRateProviderBHelper : ExchangeRateProviderHelperBase
    {
        public ExchangeRateProviderBHelper(string json) : base(json) { }

        public override IEnumerable<string> GetCurrencyPairs()
        {
            var jsonObj = JObject.Parse(base.Json);
            var properties = jsonObj.Properties().ToList();
            foreach (var p in properties)
            {
                yield return p.Name;
            }
        }

        public static bool CanParse(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override decimal? GetRateForDate(string currencyPair, DateTime day)
        {
            try
            {
                JObject jsonObj = JObject.Parse(base.Json);
                if (jsonObj.GetValue(currencyPair) == null)
                {
                    return null;
                }
                string jsonValue = jsonObj.GetValue(currencyPair).ToString();
                List<RatePerDate>? rates = JsonConvert.DeserializeObject<List<RatePerDate>>(jsonValue);
                var rateForTheDay = rates?.SingleOrDefault(x => DateTime.Parse(x.Date).Date == day.Date);
                return rateForTheDay?.Rate;
            }
            catch
            {
                throw new ArgumentException($"{base.Json} Cannot be parse by ExchangeRateProviderBHelper ");
            }
        }
    }
}
