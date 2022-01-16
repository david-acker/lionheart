using System.Data;

namespace Lionheart.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Gets an open database transaction.
    /// </summary>
    IDbTransaction StartTransaction();

    /// <summary>
    /// Commits the open database transaction.
    /// </summary>
    void CommitTransaction();
}
