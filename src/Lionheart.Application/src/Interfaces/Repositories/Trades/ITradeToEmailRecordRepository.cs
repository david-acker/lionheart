namespace Lionheart.Application.Interfaces.Repositories.Trades;

public interface ITradeToEmailRecordRepository
{
    /// <summary>
    /// Inserts a trade to email record entry.
    /// </summary>
    /// <param name="tradeId">The trade identifier.</param>
    /// <param name="emailRecordId">The email record identifier.</param>
    Task Insert(int tradeId, int emailRecordId);

    /// <summary>
    /// Deletes a trade to email record entry.
    /// </summary>
    /// <param name="tradeId">The trade identifier.</param>
    /// <param name="emailRecordId">The email record identifier.</param>
    Task Delete(int tradeId, int emailRecordId);
}
