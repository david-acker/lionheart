using Lionheart.Application;
using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Pipeline.Extraction;

public interface ITradeExtractionEngine<TTrade> where TTrade : IBaseTrade
{
    /// <summary>
    /// Attempt to extract a trade from the provided email, using
    /// the available extraction strategies.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>
    /// The extracted trade, if extraction was successful;
    /// otherwise, <c>null</c>.
    /// </returns>
    TTrade? Extract(EmailInputDomainModel email);
}

public sealed class TradeExtractionEngine<TTrade> : 
    ITradeExtractionEngine<TTrade> where TTrade : class, IBaseTrade
{
    private readonly ILionheartLogger _logger;
    private readonly IEnumerable<ITradeExtractor<TTrade>> _extractors;

    public TradeExtractionEngine(ILionheartLogger logger,
        IEnumerable<ITradeExtractor<TTrade>> extractors)
    {
        _logger = logger;
        _extractors = extractors;
    }

    /// <inheritdoc />
    public TTrade? Extract(EmailInputDomainModel email)
    {
        foreach (var strategy in _extractors)
        {
            TTrade? trade = strategy.Extract(email);

            if (trade != null)
            {
                return trade;
            }
        }

        _logger.LogWarning($"No matching extractors for email {email.SourceEmailId}.");
        return null;
    }
}
