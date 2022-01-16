using Lionheart.Application.Interfaces.Repositories.Instruments;
using Lionheart.Core.DomainModels.Instruments;
using System.Data;

namespace Lionheart.Application.Services.Instruments;

public interface IWritableEquityService
{
    /// <summary>
    /// Gets the equity identifier by ticker, creating a new
    /// equity if one does not exist.
    /// </summary>
    /// <param name="equityTicker">The equity's ticker.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The equity identifier.</returns>
    Task<int> GetEquityId(string equityTicker, IDbTransaction? transaction = null);
}

public class EquityService : IWritableEquityService
{
    private readonly IEquityRepository _equityRepository;

    public EquityService(IEquityRepository equityRepository)
    {
        _equityRepository = equityRepository;
    }

    public async Task<int> GetEquityId(string equityTicker, IDbTransaction? transaction = null)
    {
        EquityDomainModel existingEquity = await _equityRepository.GetByTicker(equityTicker, transaction);

        if (existingEquity != null)
        {
            return existingEquity.EquityId;
        }

        return await _equityRepository.Insert(equityTicker, transaction);
    }
}
