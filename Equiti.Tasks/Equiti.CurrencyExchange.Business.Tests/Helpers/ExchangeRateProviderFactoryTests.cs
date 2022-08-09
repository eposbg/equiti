using Equiti.CurrencyExchange.Business.Helpers;
using System.IO;
using Xunit;

namespace Equiti.CurrencyExchange.Business.Tests.Helpers
{
    public class ExchangeRateProviderFactoryTests
    {
        [Fact]
        public void Create_ShouldCreateTheRightTypeOfObjectDependingOnProviderADataJsonFile() {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");

            // Act
            var factory = new ExchangeRateProviderFactory();
            var result = factory.Create(json);

            // Assert
            Assert.IsType<ExchangeRateProviderAHelper>(result);
        }

        [Fact]
        public void Create_ShouldCreateTheRightTypeOfObjectDependingOnProviderBDataJsonFile()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");

            // Act
            var factory = new ExchangeRateProviderFactory();
            var result = factory.Create(json);

            // Assert
            Assert.IsType<ExchangeRateProviderBHelper>(result);
        }
    }
}
