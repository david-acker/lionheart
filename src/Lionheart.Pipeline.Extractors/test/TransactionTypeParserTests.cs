using Lionheart.Core.Trades.Enums;
using Xunit;

namespace Lionheart.Pipeline.Extractors;

public class TransactionTypeParserTests
{
    #region Test Data
    public static IEnumerable<object[]> GetValidOpenTransactionTypeInput()
    {
        var tradeActions = new string[]
        {
            "open",
            " open ",
            "Open",
            "buy",
            " buy ",
            "Buy"
        };

        foreach (string tradeAction in tradeActions)
        {
            yield return new object[] { tradeAction, TransactionType.Open };
        }
    }

    public static IEnumerable<object[]> GetValidCloseTransactionTypeInput()
    {
        var tradeActions = new string[]
        {
            "close",
            " close ",
            "Close",
            "sell",
            " sell ",
            "Sell"
        };

        foreach (string tradeAction in tradeActions)
        {
            yield return new object[] { tradeAction, TransactionType.Close };
        }
    }
    #endregion Test Data

    [Theory]
    [MemberData(nameof(GetValidOpenTransactionTypeInput))]
    [MemberData(nameof(GetValidCloseTransactionTypeInput))]
    public void Parse_ValidTradeAction_ShouldReturnCorrespondingTransactionType(string tradeAction, TransactionType expected)
    {
        // Act
        TransactionType actual = TransactionTypeParser.Parse(tradeAction);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_InvalidTradeAction_ShouldReturnUnknownTransactionType()
    {
        // Act
        TransactionType actual = TransactionTypeParser.Parse("abc");

        // Assert
        Assert.Equal(TransactionType.Unknown, actual);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Parse_NullOrBlankTradeAction_ShouldReturnUnknownTransactionType(string? tradeAction)
    {
        // Act
        TransactionType actual = TransactionTypeParser.Parse(tradeAction);

        // Assert
        Assert.Equal(TransactionType.Unknown, actual);
    }
}
