using Equiti.CurrencyExchange.Business.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Equiti.CurrencyExchange.Business.Tests.Services
{
    public class CurrencyCleanupServiceTests
    {
        [Fact]
        public async Task Should_GetLowestCurrencyRatesAsync_WhenValidData_ReturnsTheRightNumberOfPairs()
        {
            // Arrange
            var jsonA = File.ReadAllText("Data/ProviderAData.json");
            var jsonB = File.ReadAllText("Data/ProviderBData.json");
            var validDate = new DateTime(2020, 10, 1);

            // Act
            var service = new CurrencyCleanupService(jsonB, jsonA);
            var pairsWithRates = await service.GetLowestCurrencyRatesAsync(validDate);

            // Assert
            Assert.NotNull(pairsWithRates);
            Assert.Equal(4, pairsWithRates.Count);
        }

        [Fact]
        public async Task Should_GetLowestCurrencyRatesAsync_WhenInvalidData_ReturnsZero()
        {
            // Arrange
            var jsonA = File.ReadAllText("Data/ProviderAData.json");
            var jsonB = File.ReadAllText("Data/ProviderBData.json");
            var invalidDate = DateTime.MaxValue;

            // Act
            var service = new CurrencyCleanupService(jsonB, jsonA);
            var pairsWithRates = await service.GetLowestCurrencyRatesAsync(invalidDate);

            // Assert
            Assert.NotNull(pairsWithRates);
            Assert.Empty(pairsWithRates);
        }

        [Fact]
        public void Should_GetLowestCurrencyRatesAsync_PassInvalidJson_ReturnsExcelption()
        {
            // Arrange
            var jsonA = File.ReadAllText("Data/ProviderAData.json");
            var jsonB = "invalid json";
            var validDate = new DateTime(2020, 10, 1);

            // Act

            // Assert
            Assert.Throws<NotSupportedException>(() => new CurrencyCleanupService(jsonB, jsonA));
        }
    }
}
