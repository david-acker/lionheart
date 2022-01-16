using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Enums.Instruments;
using Lionheart.Core.Trades.Enums;
using Lionheart.Pipeline.Extraction;
using System.Text.RegularExpressions;

namespace Lionheart.Pipeline.Extractors.Cryptos;

public sealed class CryptoTradeExtractor : BaseTradeExtractor<CryptoTradeDomainModel>
{
    public CryptoTradeExtractor() : base() { }

    /// <inheritdoc />
    protected override string Pattern => 
        @"^Your order to (buy|sell) \$([0-9,]+\.[0-9]+) of ([a-zA-Z ]+) was executed at an average price of \$([0-9,]+\.[0-9]+) per [a-zA-Z ]+ on ([a-zA-Z]+ [0-9]{1,2}(?:st|nd|rd|th), [0-9]{4} at [0-9]{1,2}:[0-9]{2} (?:AM|PM)) (ET|CT|MT|PT)\.$";

    /// <inheritdoc />
    protected override string GetTradeDataSection(string emailBody)
    {
        if (string.IsNullOrWhiteSpace(emailBody))
        {
            return string.Empty;
        }

        // Find the start of the trade data section.
        int tradeDataStartIndex = emailBody.IndexOf("Your order to ");
        if (tradeDataStartIndex == -1)
        {
            return string.Empty;
        }

        // Find the end of the trade data section.
        int tradeDataEndIndex = emailBody.IndexOf("</div>", tradeDataStartIndex);
        if (tradeDataEndIndex == -1)
        {
            return string.Empty;
        }

        return emailBody[tradeDataStartIndex..tradeDataEndIndex].Trim();
    }

    /// <inheritdoc />
    protected override CryptoTradeDomainModel CreateTrade(Match match)
    {
        TransactionType transactionType =
            TransactionTypeParser.Parse(match.Groups[1].Value);

        decimal notional = decimal.TryParse(
            match.Groups[2].Value, out notional) ? notional : default;

        Crypto crypto = CryptoParser.Parse(match.Groups[3].Value);

        decimal averagePrice = decimal.TryParse(
            match.Groups[4].Value, out averagePrice) ? averagePrice : default;

        DateTime executedAt = TradeDateParser.Parse(match.Groups[5].Value);

        decimal quantity = averagePrice == default
            ? default
            : decimal.Round(notional / averagePrice, 6);

        return new CryptoTradeDomainModel
        {
            OrderType = OrderType.NotSpecified,
            ExecutedAt = executedAt,
            TransactionType = transactionType,
            Quantity = quantity,
            AveragePrice = averagePrice,
            Notional = notional,
            Crypto = crypto
        };
    }
}
