using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;
using Xunit;

namespace Lionheart.Pipeline.Extractors.Equities;

public class InDollarsEquityTradeExtractorTests
{
    #region Test Data
    public static IEnumerable<object[]> WellFormedEmailBodyData()
    {
        yield return new object[]
        {
            new EmailInputDomainModel
            {
                Body = "abcdefg <div>"
                    + "Your market order to buy $1,543.20 of ABC was executed on "
                    + "March 21st, 2021 at 2:30 PM. You paid $1,543.20 for 1.25 shares, "
                    + "at an average price of $1,234.56 per share."
                    + "</div> abcdefg"
            },
            new EquityTradeDomainModel
            {
                OrderType = OrderType.Market,
                ExecutedAt = new DateTime(2021, 3, 21, 14, 30, 0),
                TransactionType = TransactionType.Open,
                Quantity = 1.25m,
                AveragePrice = 1234.56m,
                Notional = 1543.20m,
                Equity = new EquityDomainModel
                {
                    Ticker = "ABC"
                }
            }
        };
        yield return new object[]
        {
            new EmailInputDomainModel
            {
                Body = "abcdefg <div>"
                    + "Your order to sell $1.25 of XYZ.A was executed on "
                    + "September 2nd, 2021 at 11:45 AM. You paid $1.25 for 0.5 shares, "
                    + "at an average price of $2.50 per share."
                    + "</div> abcdefg"
            },
            new EquityTradeDomainModel
            {
                OrderType = OrderType.Market,
                ExecutedAt = new DateTime(2021, 9, 2, 11, 45, 0),
                TransactionType = TransactionType.Close,
                Quantity = 0.5m,
                AveragePrice = 2.50m,
                Notional = 1.25m,
                Equity = new EquityDomainModel
                {
                    Ticker = "XYZ.A"
                }
            }
        };
    }
    #endregion Test Data

    [Theory]
    [MemberData(nameof(WellFormedEmailBodyData))]
    public void Extract_WellFormedEmailBody_ShouldReturnExtractedEquityTrade(EmailInputDomainModel email, EquityTradeDomainModel expectedTrade)
    {
        // Arrange
        var extractor = new InDollarsEquityTradeExtractor();

        // Act
        EquityTradeDomainModel? actualTrade = extractor.Extract(email);

        // Assert
        Assert.NotNull(actualTrade);

        Assert.Equal(expectedTrade.OrderType, actualTrade!.OrderType);
        Assert.Equal(expectedTrade.ExecutedAt, actualTrade.ExecutedAt);
        Assert.Equal(expectedTrade.TransactionType, actualTrade.TransactionType);
        Assert.Equal(expectedTrade.Quantity, actualTrade.Quantity);
        Assert.Equal(expectedTrade.AveragePrice, actualTrade.AveragePrice);
        Assert.Equal(expectedTrade.Notional, actualTrade.Notional);

        EquityDomainModel equity = actualTrade.Equity;
        Assert.NotNull(equity);
        Assert.Equal(expectedTrade.Equity.Ticker, equity.Ticker);
    }

    [Theory]
    [InlineData("<div></div>")]
    [InlineData("<div>Your market order to . . .")]
    public void Extract_TradeDataSectionNotFound_ShouldReturnNull(string emailBody)
    {
        // Arrange
        var email = new EmailInputDomainModel { Body = emailBody };

        var extractor = new InDollarsEquityTradeExtractor();

        // Act
        EquityTradeDomainModel? trade = extractor.Extract(email);

        // Assert
        Assert.Null(trade);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Extract_InvalidEmailBody_ShouldReturnNull(string emailBody)
    {
        // Arrange
        var email = new EmailInputDomainModel { Body = emailBody };

        var extractor = new InDollarsEquityTradeExtractor();

        // Act
        EquityTradeDomainModel? trade = extractor.Extract(email);

        // Assert
        Assert.Null(trade);
    }
}
