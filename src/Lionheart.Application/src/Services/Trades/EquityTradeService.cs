using Lionheart.Application.Interfaces.Repositories;
using Lionheart.Application.Interfaces.Repositories.Trades;
using Lionheart.Application.Interfaces.Services.Trades;
using Lionheart.Application.Services.Instruments;
using Lionheart.Core.DomainModels.Trades;
using System.Data;

namespace Lionheart.Application.Services.Trades;

public class EquityTradeService : ITradeService<EquityTradeDomainModel>
{
    private readonly IWritableEquityService _equityService;
    private readonly IEquityTradeRepository _equityTradeRepository;
    private readonly IBaseTradeRepository _tradeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EquityTradeService(IWritableEquityService writableEquityService,
        IEquityTradeRepository equityTradeRepository,
        IBaseTradeRepository tradeRepository,
        IUnitOfWork unitOfWork)
    {
        _equityService = writableEquityService;
        _equityTradeRepository = equityTradeRepository;
        _tradeRepository = tradeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Insert(EquityTradeDomainModel equityTrade)
    {
        using IDbTransaction transaction = _unitOfWork.StartTransaction();

        int tradeId = await _tradeRepository.Insert(equityTrade, transaction);
        int equityId = await _equityService.GetEquityId(equityTrade.Equity.Ticker, transaction);

        await _equityTradeRepository.Insert(tradeId, equityId);

        transaction.Commit();

        return tradeId;
    }
}
