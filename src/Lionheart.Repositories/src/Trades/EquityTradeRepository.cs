using Dapper;
using Lionheart.Application.Interfaces.Repositories.Trades;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.EntityModel.Trades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lionheart.Repositories.Trades;

public class EquityTradeRepository : IEquityTradeRepository
{
    public EquityTradeRepository()
    {
    }

    public async Task<int> Insert(int baseTradeId, int equityId, IDbTransaction? transaction = null)
    {
        throw new NotImplementedException();

        //var sql = @"
        //    INSERT INTO trades.EquityTrade
        //    (
        //        BaseTradeId,
        //        EquityId
        //    )
        //    VALUES
        //    (
        //        @BaseTradeId,
        //        @EquityId
        //    )";

        //var parameters = new DynamicParameters();
        //parameters.Add("BaseTradeId", baseTradeId);
        //parameters.Add("EquityId", equityId);

        //await _connection.ExecuteAsync(sql, parameters, transaction);
    }

    public async Task<EquityTradeDomainModel> GetByBaseTradeId(int tradeId, int userId)
    {
        throw new NotImplementedException();

        //var sql = @"
        //    SELECT
        //      et.*,
        //      t.*,
        //      e.*
        //    FROM trade.EquityTrade et
        //    INNER JOIN trade.BaseTrade bt 
        //        ON bt.BaseTradeId = et.BaseTradeId 
        //        AND bt.DateDeleted IS NULL
        //    INNER JOIN instruments.Equity e
        //        ON e.EquityId = et.EquityId
        //        AND e.DateDeleted IS NULL
        //    WHERE bt.BaseTradeId = @BaseTradeId 
        //        AND bt.UserId = @UserId 
        //        AND et.DateDeleted IS NULL";

        //var parameters = new DynamicParameters();
        //parameters.Add("TradeId", tradeId);
        //parameters.Add("UserId", userId);

        //EquityTrade equityTrade = (await Connection.QueryAsync(sql,
        //    types: new[]
        //    {
        //        typeof(EquityTrade),
        //        typeof(Trade),
        //        typeof(OrderType),
        //        typeof(TransactionType),
        //        typeof(Equity)
        //    },
        //    objects =>
        //    {
        //        var equityTrade = objects[0] as EquityTrade;
        //        var trade = objects[1] as Trade;
        //        var orderType = objects[2] as OrderType;
        //        var transactionType = objects[3] as TransactionType;
        //        var equity = objects[4] as Equity;

        //        trade!.OrderType = orderType;
        //        trade!.TransactionType = transactionType;

        //        equityTrade!.Trade = trade;
        //        equityTrade!.Equity = equity;

        //        return equityTrade;
        //    },
        //    parameters,
        //    splitOn: "EquityTradeId, TradeId, OrderTypeId, TransactionTypeId, EquityId")).SingleOrDefault();

        //return equityTrade?.ToDomainModel();
    }

    public Task<IEnumerable<EquityTradeDomainModel>> GetAllByUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<EquityTradeDomainModel> GetByBaseTradeId(int baseTradeId)
    {
        throw new NotImplementedException();
    }

    public Task<EquityTradeDomainModel> GetForEquityTradeId(int equityTradeId)
    {
        throw new NotImplementedException();
    }

    public Task Insert(int baseTradeId, int equityId)
    {
        throw new NotImplementedException();
    }
}
