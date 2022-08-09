namespace Equiti.CurrencyExchange.Business.Helpers
{
    public class ExchangeRateProviderFactory
    {
        public IExchangeRateProviderHelper Create(string json)
        {
            if (ExchangeRateProviderAHelper.CanParse(json)) { 
                return new ExchangeRateProviderAHelper(json);
            }

            if (ExchangeRateProviderBHelper.CanParse(json))
            {
                return new ExchangeRateProviderBHelper(json);
            }


            throw new NotSupportedException($"There is no helper defined which can handle the following JSON: {json}");
        }
    }
}
