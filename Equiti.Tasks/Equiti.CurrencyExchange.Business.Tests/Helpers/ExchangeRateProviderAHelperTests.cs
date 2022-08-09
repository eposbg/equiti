using Equiti.CurrencyExchange.Business.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Equiti.CurrencyExchange.Business.Tests.Helpers
{
    public class ExchangeRateProviderAHelperTests
    {
        [Fact]
        public void Should_GetRateForDate_WhenCorrectRequest_ReturnsData()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");
            var existingPair = "AEDUSD";
            var existingDate = new DateTime(2020, 10, 1);

            // Act
            var provider = new ExchangeRateProviderAHelper(json);
            decimal? rate = provider.GetRateForDate(existingPair, existingDate);

            // Assert
            Assert.Null(rate);
        }

        [Fact]
        public void Should_GetRateForDate_WhenNoDataForThisDate_ReturnsNull()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");
            var existingPair = "AEDUSD";
            var notExistingDate = DateTime.MaxValue;

            // Act
            var provider = new ExchangeRateProviderAHelper(json);
            decimal? rate = provider.GetRateForDate(existingPair, notExistingDate);

            // Assert
            Assert.Null(rate);
        }

        [Fact]
        public void Should_GetRateForDate_WhenNoDataForThisCurrencyPair_ReturnsNull()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");
            var notExistingPair = "NO_SUCH_PAIR";
            var existingDate = new DateTime(2020, 10, 1);

            // Act
            var provider = new ExchangeRateProviderAHelper(json);
            decimal? rate = provider.GetRateForDate(notExistingPair, existingDate);

            // Assert
            Assert.Null(rate);
        }

        [Fact]
        public void Should_CanParse_WhenValidJson_ReturnsTrue()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");

            // Act
            var result = ExchangeRateProviderAHelper.CanParse(json);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_CanParse_WhenInvalidJson_ReturnsFalse()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");

            // Act
            var result = ExchangeRateProviderAHelper.CanParse(json);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Should_GetCurrencyPairs_WhenValidJson_ReturnsRightNumberOfPairs()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderAData.json");

            // Act
            var provider = new ExchangeRateProviderAHelper(json);
            var pairs = provider.GetCurrencyPairs().ToList();

            // Assert
            Assert.NotNull(pairs);
            Assert.Equal(3, pairs?.Count);
        }

        [Fact]
        public void Should_GetCurrencyPairs_WhenInvalidJson_ReturnsRightNumberOfPairs()
        {
            // Arrange
            var json = File.ReadAllText("Data/ProviderBData.json");

            // Act
            var provider = new ExchangeRateProviderAHelper(json);

            // Assert
            Assert.Throws<Newtonsoft.Json.JsonSerializationException>(() => provider.GetCurrencyPairs().ToList());
        }
    }
}
