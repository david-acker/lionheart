using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Pipeline.Extraction;

public interface ITradeExtractor<TTrade> where TTrade : class, IBaseTrade
{ 
    /// <summary>
    /// Attempts to extract a trade from the provided email.
    /// </summary>
    /// <param name="email">The email.</param>
    TTrade? Extract(EmailInputDomainModel email);
}
