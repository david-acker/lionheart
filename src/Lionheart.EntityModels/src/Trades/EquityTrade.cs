using Lionheart.EntityModel.Instruments;

namespace Lionheart.EntityModel.Trades;

public class EquityTrade
{
    public int EquityTradeId { get; set; }

    public int EquityId { get; set; }

    public Equity Equity { get; set; }

    public int TradeId { get; set; }

    public Trade Trade { get; set; }
}

