using Lionheart.Core.Trades.Enums;

namespace Lionheart.Core.DomainModels.Trades;

/// <summary>
/// The base properties that must be implemented by
/// by all derived trade types.
/// </summary>
public interface IBaseTrade
{
    /// <summary>The unique identifier for the base trade.</summary>
    int BaseTradeId { get; set; }

    /// <summary>The unique identifier for the associated user.</summary>
    int? UserId { get; set; }

    /// <summary>The time of trade execution.</summary>
    DateTime ExecutedAt { get; set; }

    /// <summary>The quantity of the instrument traded.</summary>
    decimal Quantity { get; set; }

    /// <summary>The average price of the .</summary>
    decimal AveragePrice { get; set; }

    /// <summary>The total value of the trade.</summary>
    decimal Notional { get; set; }

    /// <summary>The order type of the trade.</summary>
    OrderType OrderType { get; set; }

    /// <summary>The transaction type of the trade.</summary>
    TransactionType TransactionType { get; set; }
}
