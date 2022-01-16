using Lionheart.Core.DomainModels.Instruments;

namespace Lionheart.Core.DomainModels.Trades;

public class EquityTradeDomainModel : BaseTradeDomainModel, IBaseTrade
{
    /// <summary>The unique identifier for the equity trade.</summary>
    public int EquityTradeId { get; set; }

    /// <summary>The unique identifier for the traded equity.</summary>
    public int EquityId { get; set; }

    public EquityDomainModel Equity { get; set; }
}
