using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Enums.Instruments;

namespace Lionheart.Application.Interfaces.Repositories.Trades;

public interface ICryptoTradeRepository
{
    /// <summary>
    /// Gets a crypto trade by its base trade identifier.
    /// </summary>
    /// <param name="baseTradeId">The base trade identifier.</param>
    Task<CryptoTradeDomainModel> GetByBaseTradeId(int baseTradeId);

    /// <summary>
    /// Gets a crypto trade by its crypto trade identifier.
    /// </summary>
    /// <param name="cryptoTradeId">The crypto trade identifier.</param>
    Task<CryptoTradeDomainModel> GetForCryptoTradeId(int cryptoTradeId);

    /// <summary>
    /// Inserts a crypto trade.
    /// </summary>
    /// <param name="baseTradeId">The base trade identifier.</param>
    /// <param name="crypto">The traded crypto.</param>
    Task Insert(int baseTradeId, Crypto crypto);
}
