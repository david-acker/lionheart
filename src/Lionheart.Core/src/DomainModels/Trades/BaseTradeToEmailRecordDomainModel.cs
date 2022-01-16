using Lionheart.Core.DomainModels.Emails;

namespace Lionheart.Core.DomainModels.Trades;

public class BaseTradeToEmailRecordDomainModel
{
    /// <summary>The unique identifier of the base trade to email record.</summary>
    public int TradeToEmailRecordId { get; set; }

    /// <summary>The unique identifier of the base trade.</summary>
    public int BaseTradeId { get; set; }

    public BaseTradeDomainModel Trade { get; set; }

    /// <summary>The unique identifier of the email record.</summary>
    public int EmailRecordId { get; set; }

    public EmailRecordDomainModel EmailRecord { get; set; }
}
