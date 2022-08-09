using Equiti.CurrencyExchange.Business.Helpers;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace Equiti.CurrencyExchange.Business.Tests.Helpers
{
    public class ExchangeRateProviderBHelperTests
    {
        [Fact]
        public void Should_GetRateForDate_WhenCorrectRequest_ReturnsData(){
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");
            var existingPair = "AEDUSD";
            var existingDate = new DateTime(2020, 10, 1);

            // Act
            var provider = new ExchangeRateProviderBHelper(json);
            decimal? rate = provider.GetRateForDate(existingPair, existingDate);

            // Assert
            Assert.Equal(0.27m, rate);
        }

        [Fact]
        public void Should_GetRateForDate_WhenNoDataForThisDate_ReturnsNull()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");
            var existingPair = "AEDUSD";
            var notExistingDate = DateTime.MaxValue;

            // Act
            var provider = new ExchangeRateProviderBHelper(json);
            decimal? rate = provider.GetRateForDate(existingPair, notExistingDate);

            // Assert
            Assert.Null(rate);
        }

        [Fact]
        public void Should_GetRateForDate_WhenNoDataForThisCurrencyPair_ReturnsNull()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");
            var notExistingPair = "NO_SUCH_PAIR";
            var existingDate = new DateTime(2020, 10, 1);

            // Act
            var provider = new ExchangeRateProviderBHelper(json);
            decimal? rate = provider.GetRateForDate(notExistingPair, existingDate);

            // Assert
            Assert.Null(rate);
        }

        [Fact]
        public void Should_CanParse_WhenValidJson_ReturnsTrue()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");

            // Act
            var result = ExchangeRateProviderBHelper.CanParse(json);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_CanParse_WhenInvalidJson_ReturnsFalse()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");

            // Act
            var result = ExchangeRateProviderBHelper.CanParse(json);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Should_GetCurrencyPairs_WhenValidJson_ReturnsRightNumberOfPairs()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");

            // Act
            var provider = new ExchangeRateProviderBHelper(json);
            var pairs = provider.GetCurrencyPairs().ToList();

            // Assert
            Assert.NotNull(pairs);
            Assert.Equal(3, pairs?.Count);
        }

        [Fact]
        public void Should_GetCurrencyPairs_WhenInvalidJson_ReturnsRightNumberOfPairs()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");

            // Act
            var provider = new ExchangeRateProviderBHelper(json);

            // Assert
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => provider.GetCurrencyPairs().ToList());
        }
    }
}
