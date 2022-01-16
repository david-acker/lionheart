using Lionheart.Core.Trades.Enums;

namespace Lionheart.Core.DomainModels.Trades;

/// <inheritdoc />
public abstract class BaseTradeDomainModel : IBaseTrade
{
    /// <inheritdoc />
    public int BaseTradeId { get; set; }

    /// <inheritdoc />
    public int? UserId { get; set; }

    /// <inheritdoc />
    public DateTime ExecutedAt { get; set; }

    /// <inheritdoc />
    public decimal Quantity { get; set; }

    /// <inheritdoc />
    public decimal AveragePrice { get; set; }

    /// <inheritdoc />
    public decimal Notional { get; set; }

    /// <inheritdoc />
    public OrderType OrderType { get; set; }

    /// <inheritdoc />
    public TransactionType TransactionType { get; set; }
}
