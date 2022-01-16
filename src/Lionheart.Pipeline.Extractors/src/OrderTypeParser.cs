using Lionheart.Core.Trades.Enums;

namespace Lionheart.Pipeline.Extractors;

internal static class OrderTypeParser
{
    /// <summary>
    /// Gets the order type corresponding to the input string.
    /// </summary>
    /// <param name="orderTypeName">The name of the order type.</param>
    /// <returns>
    /// The <c>OrderType</c> matching the input string;
    /// otherwise, <c>OrderType.Unknown</c>.
    /// </returns>
    public static OrderType Parse(string? orderTypeName)
    {
        return (orderTypeName?.Trim().ToLower()) switch
        {
            "limit" => OrderType.Limit,

            "market" => OrderType.Market,

            _ => OrderType.Unknown
        };
    }
}
