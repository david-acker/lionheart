namespace Lionheart.Core.DomainModels.Instruments;

public class EquityDomainModel
{
    /// <summary>The unique identifier for the equity.</summary>
    public int EquityId { get; set; }

    /// <summary>The ticker associated with the equity.</summary>
    public string Ticker { get; set; }
}
