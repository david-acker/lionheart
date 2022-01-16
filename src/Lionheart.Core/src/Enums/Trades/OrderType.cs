namespace Lionheart.Core.Trades.Enums;

public enum OrderType
{
    Unknown,

    /// <summary>Filled at the best current market price.</summary>
    Market,

    /// <summary>Filled at the specified limit price.</summary>
    Limit,

    /// <summary>Filled either at the best current market price or a specified limit price.</summary>
    /// <remarks>
    /// Used for crypto trades which allow both market and limit orders
    /// but don't specifiy the OrderType in their trade execution emails.
    /// </remarks>
    NotSpecified
}
