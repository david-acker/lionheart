using Lionheart.Core.Trades.Enums;
using Xunit;

namespace Lionheart.Pipeline.Extractors;

public class OrderTypeHelperTests
{
    #region Test Data
    public static IEnumerable<object[]> GetValidLimitOrderTypeInput()
    {
        yield return new object[] { "limit", OrderType.Limit };
        yield return new object[] { " limit ", OrderType.Limit };
        yield return new object[] { "LIMIT", OrderType.Limit };
    }

    public static IEnumerable<object[]> GetValidMarketOrderTypeInput()
    {
        yield return new object[] { "market", OrderType.Market };
        yield return new object[] { " market ", OrderType.Market };
        yield return new object[] { "MARKET", OrderType.Market };
    }
    #endregion Test Data

    [Theory]
    [MemberData(nameof(GetValidLimitOrderTypeInput))]
    [MemberData(nameof(GetValidMarketOrderTypeInput))]
    public void Parse_ValidOrderTypeInput_ShouldReturnCorrespondingOrderType(string orderTypeName, OrderType expected)
    {
        // Act
        OrderType actual = OrderTypeParser.Parse(orderTypeName);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_InvalidOrderTypeInput_ShouldReturnUnknownOrderType()
    {
        // Act
        OrderType actual = OrderTypeParser.Parse("abc");

        // Assert
        Assert.Equal(OrderType.Unknown, actual);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Parse_NullOrBlankInput_ShouldReturnUnknownOrderType(string? orderTypeName)
    {
        // Act
        OrderType actual = OrderTypeParser.Parse(orderTypeName);

        // Assert
        Assert.Equal(OrderType.Unknown, actual);
    }
}
