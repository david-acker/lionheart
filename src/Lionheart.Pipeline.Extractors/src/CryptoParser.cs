using Lionheart.Core.Enums.Instruments;

namespace Lionheart.Pipeline.Extractors;

internal static class CryptoParser
{
    /// <summary>
    /// Gets the crypto corresponding to the input string.
    /// </summary>
    /// <param name="cryptoName">The name of the crypto.</param>
    /// <returns>
    /// The <c>Crypto</c> matching the input string;
    /// otherwise, <c>Crypto.Unknown</c>.
    /// </returns>
    public static Crypto Parse(string? cryptoName)
    {
        return (cryptoName?.Trim().ToLower()) switch
        {
            "bitcoin" => Crypto.Bitcoin,

            "bitcoin cash" => Crypto.BitcoinCash,

            "bitcoin sv" => Crypto.BitcoinSV,

            "ethereum" => Crypto.Ethereum,

            "ethereum classic" => Crypto.EthereumClassic,

            "litecoin" => Crypto.Litecoin,

            "dogecoin" => Crypto.Dogecoin,

            _ => Crypto.Unknown
        };
    }
}
