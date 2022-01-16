using Lionheart.Core.Trades.Enums;

namespace Lionheart.Pipeline.Extractors;

internal static class TransactionTypeParser
{
    /// <summary>
    /// Get the transaction type corresponding to the string
    /// representation of the trade action.
    /// </summary>
    /// <param name="tradeAction">The trade action.</param>
    /// <returns>
    /// The <c>TransactionType</c> corresponding to the trade action;
    /// otherwise, <c>TransactionType.Unknown</c>.
    /// </returns>
    public static TransactionType Parse(string? tradeAction)
    {
        return (tradeAction?.Trim().ToLower()) switch
        {
            "buy" or "open" => TransactionType.Open,
            "sell" or "close" => TransactionType.Close,
            _ => TransactionType.Unknown,
        };
    }
}
