using Lionheart.Core.DomainModels.Trades;
using System.Data;

namespace Lionheart.Application.Interfaces.Repositories.Trades;

public interface IBaseTradeRepository
{
    /// <summary>
    /// Inserts the base trade entity.
    /// </summary>
    /// <param name="trade">The trade.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The identifier of the inserted base trade.</returns>
    Task<int> Insert(IBaseTrade trade, IDbTransaction? transaction = null);
}
