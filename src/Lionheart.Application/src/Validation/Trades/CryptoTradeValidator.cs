using Lionheart.Application.Services.Trades;
using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Application.Validation.Trades;

public class CryptoTradeValidator : BaseTradeValidator<CryptoTradeDomainModel>
{
    public override Dictionary<string, IEnumerable<string>> Validate(CryptoTradeDomainModel trade)
    {
        throw new NotImplementedException();
    }
}
