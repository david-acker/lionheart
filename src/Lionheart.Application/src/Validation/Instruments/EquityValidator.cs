using Lionheart.Core.DomainModels.Instruments;

namespace Lionheart.Application.Services.Instruments;

public class EquityValidator : BaseValidator<EquityDomainModel>
{
    private const int _maximumTickerLength = 10;

    public EquityValidator() : base() { }

    protected override void AddFieldValidators()
    {
        FieldValidators.Add(nameof(EquityDomainModel.Ticker), ValidateTicker);
    }

    private IEnumerable<string> ValidateTicker(EquityDomainModel equityDomainModel)
    {
        var tickerErrorMessages = new List<string>();

        if (string.IsNullOrWhiteSpace(equityDomainModel.Ticker))
        {
            tickerErrorMessages.Add($"{nameof(EquityDomainModel.Ticker)} cannot be blank.");
        }
        else if (equityDomainModel.Ticker.Length > _maximumTickerLength)
        {
            tickerErrorMessages.Add($"{nameof(EquityDomainModel.Ticker)} cannot exceed {_maximumTickerLength} characters.");
        }

        return tickerErrorMessages;
    }
}
