namespace Lionheart.Core.Trades.Enums;

public enum TransactionType
{
    Unknown,

    /// <summary>Opens a new position.</summary>
    /// <remarks>
    /// Equivalent to buying units of the associated instrument, 
    /// as Robinhood does not currently allow short selling.
    /// </remarks>
    Open,

    /// <summary>Closes an existing position.</summary>
    /// <remarks>
    /// Equivalent to selling units of the associated instrument, 
    /// as Robinhood does not currently allow short selling.
    /// </remarks>
    Close
}
