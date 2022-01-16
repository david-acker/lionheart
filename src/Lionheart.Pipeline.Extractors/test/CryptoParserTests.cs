using Lionheart.Core.Enums.Instruments;
using Xunit;

namespace Lionheart.Pipeline.Extractors.Test;

public class CryptoParserTests
{
    #region Test Data
    public static IEnumerable<object[]> GetValidBitcoinInput()
    {
        yield return new object[] { "Bitcoin", Crypto.Bitcoin };
        yield return new object[] { " Bitcoin ", Crypto.Bitcoin };
        yield return new object[] { "BITCOIN", Crypto.Bitcoin };
    }

    public static IEnumerable<object[]> GetValidBitcoinCashInput()
    {
        yield return new object[] { "Bitcoin Cash", Crypto.BitcoinCash };
        yield return new object[] { " Bitcoin Cash ", Crypto.BitcoinCash };
        yield return new object[] { "BITCOIN CASH", Crypto.BitcoinCash };
    }

    public static IEnumerable<object[]> GetValidBitcoinSVInput()
    {
        yield return new object[] { "Bitcoin SV", Crypto.BitcoinSV };
        yield return new object[] { " Bitcoin SV ", Crypto.BitcoinSV };
        yield return new object[] { "BITCOIN SV", Crypto.BitcoinSV };
    }

    public static IEnumerable<object[]> GetValidEthereumInput()
    {
        yield return new object[] { "Ethereum", Crypto.Ethereum };
        yield return new object[] { " Ethereum ", Crypto.Ethereum };
        yield return new object[] { "ETHEREUM", Crypto.Ethereum };
    }

    public static IEnumerable<object[]> GetValidEthereumClassicInput()
    {
        yield return new object[] { "Ethereum Classic", Crypto.EthereumClassic };
        yield return new object[] { " Ethereum Classic ", Crypto.EthereumClassic };
        yield return new object[] { "ETHEREUM CLASSIC", Crypto.EthereumClassic };
    }

    public static IEnumerable<object[]> GetValidLitecoinInput()
    {
        yield return new object[] { "Litecoin", Crypto.Litecoin };
        yield return new object[] { " Litecoin ", Crypto.Litecoin };
        yield return new object[] { "LITECOIN", Crypto.Litecoin };
    }

    public static IEnumerable<object[]> GetValidDogecoinInput()
    {
        yield return new object[] { "Dogecoin", Crypto.Dogecoin };
        yield return new object[] { " Dogecoin ", Crypto.Dogecoin };
        yield return new object[] { "DOGECOIN", Crypto.Dogecoin };
    }
    #endregion Test Data

    [Theory]
    [MemberData(nameof(GetValidBitcoinInput))]
    [MemberData(nameof(GetValidBitcoinCashInput))]
    [MemberData(nameof(GetValidBitcoinSVInput))]
    [MemberData(nameof(GetValidEthereumInput))]
    [MemberData(nameof(GetValidEthereumClassicInput))]
    [MemberData(nameof(GetValidLitecoinInput))]
    [MemberData(nameof(GetValidDogecoinInput))]
    public void Parse_ValidCryptoInput_ShouldReturnCorrespondingCrypto(string cryptoName, Crypto expected)
    {
        // Act
        Crypto actual = CryptoParser.Parse(cryptoName);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_InvalidCryptoInput_ShouldReturnUnknownCrypto()
    {
        // Act
        Crypto actual = CryptoParser.Parse("abc");

        // Assert
        Assert.Equal(Crypto.Unknown, actual);
    }

    [Theory]
    [InlineData("  ")]
    [InlineData("")]
    [InlineData(null)]
    public void Parse_NullOrBlankCryptoName_ShouldReturnUnknownCrypto(string? cryptoName)
    {
        // Act
        Crypto actual = CryptoParser.Parse(cryptoName);

        // Assert
        Assert.Equal(Crypto.Unknown, actual);
    }
}

