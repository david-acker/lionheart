using Lionheart.Core.DomainModels.Emails;
using System.Data;

namespace Lionheart.Application.Interfaces.Repositories.Emails;

public interface ISenderRepository
{
    /// <summary>
    /// Gets the sender by email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="transaction">The database transaction.</param>
    Task<SenderDomainModel> GetByEmail(string emailAddress, IDbTransaction? transaction = null);

    /// <summary>
    /// Inserts a sender using the provided email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The identifier of the inserted sender.</returns>
    Task<int> Insert(string emailAddress, IDbTransaction? transaction = null);
}
