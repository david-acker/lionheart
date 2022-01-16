using Lionheart.Application.Interfaces.Repositories.Trades;

namespace Lionheart.Application.Services.Trades;

public interface ITradeToEmailRecordService
{
    /// <summary>
    /// Links the trade to the email record.
    /// </summary>
    /// <param name="tradeId">The trade identifier.</param>
    /// <param name="emailRecordId">The email record identifier.</param>
    Task Link(int tradeId, int emailRecordId);

    /// <summary>
    /// Unlinks the trade from the email record.
    /// </summary>
    /// <param name="tradeId">The trade identifier.</param>
    /// <param name="emailRecordId">The email record identifier.</param>
    Task Unlink(int tradeId, int emailRecordId);
}

public class TradeToEmailRecordService : ITradeToEmailRecordService
{
    private readonly ITradeToEmailRecordRepository _tradeToEmailRecordRepository;

    public TradeToEmailRecordService(ITradeToEmailRecordRepository tradeToEmailRecordRepository)
    {
        _tradeToEmailRecordRepository = tradeToEmailRecordRepository;
    }

    public async Task Link(int tradeId, int emailRecordId)
    {
        await _tradeToEmailRecordRepository.Insert(tradeId, emailRecordId);
    }

    public async Task Unlink(int tradeId, int emailRecordId)
    {
        await _tradeToEmailRecordRepository.Delete(tradeId, emailRecordId);
    }
}
