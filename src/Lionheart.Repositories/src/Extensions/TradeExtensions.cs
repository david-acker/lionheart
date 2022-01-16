using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.EntityModel.Trades;

namespace Lionheart.Repositories.Extensions;

public static class TradeExtensions
{
    public static EquityTradeDomainModel ToDomainModel(this EquityTrade equityTrade)
    {
        if (equityTrade.Trade == null)
        {
            throw new InvalidOperationException($"Cannot create an {nameof(EquityTradeDomainModel)} instance if the source {nameof(EquityTrade.Trade)} is null.");
        }

        if (equityTrade.Equity == null)
        {
            throw new InvalidOperationException($"Cannot create an {nameof(EquityTradeDomainModel)} instance if the source {nameof(EquityTrade.Equity)} is null.");
        }

        return new EquityTradeDomainModel
        {
            EquityTradeId = equityTrade.EquityTradeId,
            EquityId = equityTrade.EquityId,
            BaseTradeId = equityTrade.Trade.TradeId,
            UserId = equityTrade.Trade.UserId,
            ExecutedAt = equityTrade.Trade.ExecutedAt,
            Quantity = equityTrade.Trade.Quantity,
            AveragePrice = equityTrade.Trade.AveragePrice,
            Notional = equityTrade.Trade.Notional,
            Equity = new EquityDomainModel
            {
                EquityId = equityTrade.Equity.EquityId,
                Ticker = equityTrade.Equity.Ticker
            }
        };
    }
}
