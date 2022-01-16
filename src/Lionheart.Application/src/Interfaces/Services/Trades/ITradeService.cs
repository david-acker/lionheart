using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Application.Interfaces.Services.Trades;

public interface ITradeService<TTrade> where TTrade : IBaseTrade
{
    /// <summary>
    /// Inserts the trade.
    /// </summary>
    /// <param name="trade">The trade.</param>
    /// <returns>The identifier of the inserted.</returns>
    Task<int> Insert(TTrade trade);
}
