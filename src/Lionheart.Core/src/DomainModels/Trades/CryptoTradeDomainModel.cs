using Lionheart.Core.Enums.Instruments;

namespace Lionheart.Core.DomainModels.Trades;

public class CryptoTradeDomainModel : BaseTradeDomainModel, IBaseTrade
{
    /// <summary>The unique identifier for the crypto trade.</summary>
    public int CryptoTradeId { get; set; }

    /// <summary>The traded cryptocurrency.</summary>
    public Crypto Crypto { get; set; }
}
