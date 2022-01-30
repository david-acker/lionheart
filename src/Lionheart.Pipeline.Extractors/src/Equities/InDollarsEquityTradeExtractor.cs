using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;
using Lionheart.Pipeline.Extraction;
using System.Text.RegularExpressions;

namespace Lionheart.Pipeline.Extractors.Equities;

public sealed class InDollarsEquityTradeExtractor : BaseTradeExtractor<EquityTradeDomainModel>
{
    public InDollarsEquityTradeExtractor() : base() { }

    /// <inheritdoc />
    protected override string Pattern => 
        @"^Your (?:market )?order to (buy|sell) \$([0-9,]+\.[0-9]+) of ([a-zA-Z\.]+) was executed on ([a-zA-Z]+ [0-9]{1,2}(?:st|nd|rd|th), [0-9]{4} at [0-9]{1,2}:[0-9]{2} (?:AM|PM))\. You (?:paid|received) \$[0-9,]+\.[0-9]+ for ([0-9,\.]+) shares?, at an average price of \$([0-9,]+\.[0-9]+) per share\.$";

    /// <inheritdoc />
    protected override string GetTradeDataSection(string emailBody)
    {
        if (string.IsNullOrWhiteSpace(emailBody))
        {
            return string.Empty;
        }

        // Find the start of the trade data section.
        int tradeDataStartIndex = emailBody.IndexOf("Your ");
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
    protected override EquityTradeDomainModel CreateTrade(Match match)
    {
        TransactionType transactionType = 
            TransactionTypeParser.Parse(match.Groups[1].Value);

        decimal notional = decimal.TryParse(
            match.Groups[2].Value, out notional) ? notional : default;

        string ticker = match.Groups[3].Value;

        DateTime executedAt = TradeDateParser.Parse(match.Groups[4].Value);

        decimal quantity = decimal.TryParse(
            match.Groups[5].Value, out quantity) ? quantity : default;

        decimal averagePrice = decimal.TryParse(
            match.Groups[6].Value, out averagePrice) ? averagePrice : default;

        return new EquityTradeDomainModel
        {
            // Fractional trades can only be placed as market orders.
            OrderType = OrderType.Market,
            ExecutedAt = executedAt,
            TransactionType = transactionType,
            Quantity = quantity,
            AveragePrice = averagePrice,
            Notional = notional,
            Equity = new EquityDomainModel
            {
                Ticker = ticker
            }
        };
    }
}
