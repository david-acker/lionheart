using Lionheart.Core.DomainModels.Emails;
using System.Data;

namespace Lionheart.Application.Interfaces.Repositories.Emails;

public interface IEmailRecordRepository
{
    /// <summary>
    /// Gets the email record by its source email identifier.
    /// </summary>
    /// <param name="sourceEmailId">The source email identifier.</param>
    /// <param name="transaction">The database transaction.</param>
    Task<EmailRecordDomainModel> GetBySourceEmailId(string sourceEmailId, IDbTransaction? transaction = null);

    /// <summary>
    /// Inserts the email record.
    /// </summary>
    /// <param name="emailRecord">The email record.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The inserted email record identifier.</returns>
    Task<int> Insert(EmailRecordDomainModel emailRecord, IDbTransaction? transaction = null);
}
