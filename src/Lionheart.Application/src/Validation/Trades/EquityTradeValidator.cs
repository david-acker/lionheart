using Lionheart.Application.Interfaces.Services;
using Lionheart.Core.DomainModels.Instruments;
using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Application.Services.Trades;

public class EquityTradeValidator : BaseTradeValidator<EquityTradeDomainModel>
{
    private readonly IValidationService<EquityDomainModel> _equityValidationService;

    public EquityTradeValidator(IValidationService<EquityDomainModel> equityValidationService) : base()
    {
        _equityValidationService = equityValidationService;
    }

    protected override void AddFieldValidators()
    {
        base.AddFieldValidators();
    }

    public override Dictionary<string, IEnumerable<string>> Validate(EquityTradeDomainModel trade)
    {
        var tradeValidationResults = ApplyValidationRules(trade);
        var equityValidationResults = _equityValidationService.Validate(trade.Equity);

        MergeValidationResults(tradeValidationResults, equityValidationResults, nameof(EquityTradeDomainModel.Equity));

        return tradeValidationResults;
    }
}
