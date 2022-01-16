using Lionheart.Core.DomainModels.Trades;

namespace Lionheart.Application.Interfaces.Repositories.Trades;

public interface IEquityTradeRepository
{
    /// <summary>
    /// Gets all equity trades for the user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    Task<IEnumerable<EquityTradeDomainModel>> GetAllByUser(int userId);

    /// <summary>
    /// Gets an equity trade by its base trade identifier.
    /// </summary>
    /// <param name="baseTradeId">The base trade identifier.</param>
    Task<EquityTradeDomainModel> GetByBaseTradeId(int baseTradeId);

    /// <summary>
    /// Gets an equity trade by its base trade identifier and its user identifier.
    /// </summary>
    /// <param name="baseTradeId">The base trade identifier.</param>
    /// <param name="userId">The user identifier.</param>
    Task<EquityTradeDomainModel> GetByBaseTradeId(int baseTradeId, int userId);

    /// <summary>
    /// Gets an equity trade by its equity trade identifier.
    /// </summary>
    /// <param name="equityTradeId">The equity trade identifier.</param>
    Task<EquityTradeDomainModel> GetForEquityTradeId(int equityTradeId);

    /// <summary>
    /// Inserts an equity trade.
    /// </summary>
    /// <param name="baseTradeId">The base trade identifier.</param>
    /// <param name="equityId">The equity identifier.</param>
    Task Insert(int baseTradeId, int equityId);
}
