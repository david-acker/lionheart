using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;
using Lionheart.Pipeline.Extraction;
using System.Text.RegularExpressions;

namespace Lionheart.Pipeline.Extractors.Equities;

public sealed class InSharesEquityTradeExtractor : BaseTradeExtractor<EquityTradeDomainModel>
{
    public InSharesEquityTradeExtractor() : base() { }

    /// <inheritdoc />
    protected override string Pattern => 
        @"^Your (limit|market) order to (buy|sell) ([0-9,\.]+) shares? of ([a-zA-Z\.]+) was executed at an average price of \$([0-9,]+\.[0-9]+) on ([a-zA-Z]+ [0-9]{1,2}(?:st|nd|rd|th), [0-9]{4} at [0-9]{1,2}:[0-9]{2} (?:AM|PM))\.$";

    /// <inheritdoc />
    protected override string GetTradeDataSection(string emailBody)
    {
        if (string.IsNullOrWhiteSpace(emailBody))
        {
            return string.Empty;
        }

        // Find the start of the trade data section.
        Match startIndexMatch = Regex.Match(emailBody, "Your (limit|market) order");
        if (!startIndexMatch.Success)
        {
            return string.Empty;
        }

        int tradeDataStartIndex = startIndexMatch.Index;

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
        OrderType orderType = 
            OrderTypeParser.Parse(match.Groups[1].Value);

        TransactionType transactionType =
            TransactionTypeParser.Parse(match.Groups[2].Value);

        decimal quantity = decimal.TryParse(
            match.Groups[3].Value, out quantity) ? quantity : default;

        string ticker = match.Groups[4].Value;

        decimal averagePrice = decimal.TryParse(
            match.Groups[5].Value, out averagePrice) ? averagePrice : default;

        DateTime executedAt = TradeDateParser.Parse(match.Groups[6].Value);

        var notional = decimal.Round(quantity * averagePrice, 2);

        return new EquityTradeDomainModel
        {
            OrderType = orderType,
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
