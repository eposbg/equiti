namespace Equiti.CurrencyExchange.Business.Helpers
{

    public interface IExchangeRateProviderHelper {
        decimal? GetRateForDate(string currencyPair, DateTime day);
        IEnumerable<string> GetCurrencyPairs();
    }

    public abstract class ExchangeRateProviderHelperBase: IExchangeRateProviderHelper
    {
        public ExchangeRateProviderHelperBase(string json)
        {
            Json=json;
        }

        public abstract decimal? GetRateForDate(string currencyPair, DateTime day);

        public abstract IEnumerable<string> GetCurrencyPairs();

        public string Json { get; }
    }
}
