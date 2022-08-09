namespace Equiti.CurrencyExchange.Business.Models
{
    public class ExchangeRateForDate
    {
        public DateTime Date { get; set; }
        public string CurrencyPair { get; set; }
        public decimal Rate { get; set; }
    }
}
