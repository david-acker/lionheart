using Lionheart.Core.DomainModels.Instruments;
using System.Data;

namespace Lionheart.Application.Interfaces.Repositories.Instruments;

public interface IEquityRepository
{
    /// <summary>
    /// Gets an equity by its ticker.
    /// </summary>
    /// <param name="ticker">The ticker.</param>
    /// <param name="transaction">The database transaction.</param>
    Task<EquityDomainModel> GetByTicker(string ticker, IDbTransaction? transaction = null);

    /// <summary>
    /// Inserts an equity using the provided ticker.
    /// </summary>
    /// <param name="ticker">The ticker.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The identifier of the inserted equity.</returns>
    Task<int> Insert(string ticker, IDbTransaction? transaction = null);
}
