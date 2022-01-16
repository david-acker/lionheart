namespace Lionheart.EntityModel.Trades;

public class Trade
{
    public int TradeId { get; set; }

    public int? UserId { get; set; }

    public int OrderTypeId { get; set; }

    public OrderType OrderType { get; set; }

    public int TransactionTypeId { get; set; }

    public TransactionType TransactionType { get; set; }

    public DateTime ExecutedAt { get; set; }

    public decimal Quantity { get; set; }

    public decimal AveragePrice { get; set; }

    public decimal Notional { get; set; }
}

