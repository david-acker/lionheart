using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;
using Xunit;

namespace Lionheart.Pipeline.Extractors.Equities;

public class InDollarsEquityTradeExtractorTests
{
    [Fact]
    public void Extract_WellFormedEmailBody_ShouldReturnExtractedEquityTrade()
    {
        // Arrange
        var tradeData = "Your market order to buy " +
            $"$1,543.20 of ABC was executed on " +
            $"March 21st, 2021 at 2:30 PM. You paid $1,543.20 for 1.25 shares, " +
            $"at an average price of $1,234.56 per share.";

        var email = new EmailInputDomainModel { Body = $"abcdefg <div>{tradeData}</div> abcdefg" };

        var extractor = new InDollarsEquityTradeExtractor();

        // Act
        EquityTradeDomainModel? trade = extractor.Extract(email);

        // Assert
        Assert.NotNull(trade);

        Assert.Equal(OrderType.Market, trade!.OrderType);
        Assert.Equal(new DateTime(2021, 3, 21, 14, 30, 0), trade.ExecutedAt);
        Assert.Equal(TransactionType.Open, trade.TransactionType);
        Assert.Equal(1.25m, trade.Quantity);
        Assert.Equal(1234.56m, trade.AveragePrice);
        Assert.Equal(1543.20m, trade.Notional);

        EquityDomainModel equity = trade.Equity;
        Assert.NotNull(equity);
        Assert.Equal("ABC", equity.Ticker);
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
