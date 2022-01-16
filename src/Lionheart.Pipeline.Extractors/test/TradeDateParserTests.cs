using Xunit;

namespace Lionheart.Pipeline.Extractors;

public class TradeDateParserTests
{
    #region Test Data
    public static IEnumerable<object[]> GetValidTradeDateInput()
    {
        yield return new object[]
        {
            "December 31st, 2020 at 10:33 AM",
            new DateTime(2020, 12, 31, 10, 33, 0)
        };
        yield return new object[]
        {
            "February 4th, 2021 at 3:16 PM",
            new DateTime(2021, 2, 4, 15, 16, 0)
        };
        yield return new object[]
        {
            "August 13th, 2021 at 9:47 AM",
            new DateTime(2021, 8, 13, 9, 47, 0)
        };
    }

    public static IEnumerable<object[]> GetInvalidTradeDateInput()
    {
        yield return new object[] { "August -1, 2021 at 12:31 PM" };
        yield return new object[] { "August 1st, 2021 at 25:31 AM" };
        yield return new object[] { "Invalid 41st, 2021 at 25:31 FM" };
    }
    #endregion Test Data

    [Theory]
    [MemberData(nameof(GetValidTradeDateInput))]
    public void Parse_WellFormedInput_ShouldReturnParsedTradeDate(string input, DateTime expected)
    {
        // Act
        DateTime actual = TradeDateParser.Parse(input);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GetInvalidTradeDateInput))]
    public void Parse_MalformedInput_ShouldReturnDateTimeMinValue(string input)
    {
        // Act
        DateTime actual = TradeDateParser.Parse(input);

        // Assert
        Assert.Equal(DateTime.MinValue, actual);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Parse_NullOrBlankInput_ShouldReturnDateTimeMinValue(string? input)
    {
        // Act
        DateTime actual = TradeDateParser.Parse(input);

        // Assert
        Assert.Equal(DateTime.MinValue, actual);
    }
}
