using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Trades;
using System.Text.RegularExpressions;

namespace Lionheart.Pipeline.Extraction;

/// <summary>
/// An abstract trade extractor for implementing new extractors.
/// </summary>
/// <typeparam name="TTrade">The specified trade type.</typeparam>
public abstract class BaseTradeExtractor<TTrade> : 
    ITradeExtractor<TTrade> where TTrade : class, IBaseTrade
{
    private readonly Regex _tradeDataRegex;

    protected BaseTradeExtractor()
    {
        _tradeDataRegex = new Regex(Pattern, RegexOptions.Compiled);
    }

    /// <summary>
    /// The RegEx pattern representing the expected format of the trade data.
    /// </summary>
    protected abstract string Pattern { get; }

    /// <summary>
    /// Gets the trade data section from the email body.
    /// </summary>
    /// <param name="emailBody">The full email body.</param>
    protected abstract string GetTradeDataSection(string emailBody);

    /// <summary>
    /// Creates a trade instance using the <see cref="Match"/> result
    /// for the trade data section.
    /// </summary>
    /// <param name="match">The regular expression match.</param>
    protected abstract TTrade CreateTrade(Match match);

    /// <inheritdoc />
    public virtual TTrade? Extract(EmailInputDomainModel email)
    {
        string tradeData = GetTradeDataSection(email.Body);

        if (string.IsNullOrWhiteSpace(tradeData))
        {
            return null;
        }

        Match match = _tradeDataRegex.Match(tradeData);

        return match.Success 
            ? CreateTrade(match) 
            : null;
    }
}
