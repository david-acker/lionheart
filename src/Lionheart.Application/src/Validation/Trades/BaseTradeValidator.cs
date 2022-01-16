using Lionheart.Core.DomainModels.Trades;
using Lionheart.Core.Trades.Enums;

namespace Lionheart.Application.Services.Trades;

/// <summary>
/// The base trade validator.
/// </summary>
/// <typeparam name="TTrade">The specified trade type.</typeparam>
public abstract class BaseTradeValidator<TTrade> : 
    BaseValidator<BaseTradeDomainModel> where TTrade : IBaseTrade
{
    public abstract Dictionary<string, IEnumerable<string>> Validate(TTrade trade);

    public BaseTradeValidator() : base() { }

    protected override void AddFieldValidators()
    {
        FieldValidators.Add(nameof(BaseTradeDomainModel.OrderType), ValidateOrderType);
        FieldValidators.Add(nameof(BaseTradeDomainModel.ExecutedAt), ValidateExecutedAt);
        FieldValidators.Add(nameof(BaseTradeDomainModel.Quantity), ValidateQuantity);
        FieldValidators.Add(nameof(BaseTradeDomainModel.AveragePrice), ValidateAveragePrice);
        FieldValidators.Add(nameof(BaseTradeDomainModel.Notional), ValidateNotional);
        FieldValidators.Add(nameof(BaseTradeDomainModel.TransactionType), ValidateTransactionType);
    }

    protected virtual IEnumerable<string> ValidateOrderType(BaseTradeDomainModel trade)
    {
        var orderTypeErrorMessages = new List<string>();

        if (trade.OrderType == OrderType.Unknown)
        {
            orderTypeErrorMessages.Add($"{nameof(BaseTradeDomainModel.OrderType)} was not specified.");
        }

        return orderTypeErrorMessages;
    }

    public virtual IEnumerable<string> ValidateExecutedAt(BaseTradeDomainModel trade)
    {
        var executedAtErrorMessages = new List<string>();

        if (trade.ExecutedAt == DateTime.MinValue)
        {
            executedAtErrorMessages.Add($"{nameof(BaseTradeDomainModel.ExecutedAt)} could not be parsed.");
        }

        return executedAtErrorMessages;
    }

    protected virtual IEnumerable<string> ValidateQuantity(BaseTradeDomainModel trade)
    {
        var quantityErrorMessages = new List<string>();

        if (trade.Quantity == 0)
        {
            quantityErrorMessages.Add($"{nameof(BaseTradeDomainModel.Quantity)} could not be parsed.");
        }
        else if (trade.Quantity < 0)
        {
            quantityErrorMessages.Add($"{nameof(BaseTradeDomainModel.Quantity)} must be greater than zero.");
        }

        return quantityErrorMessages;
    }

    protected virtual IEnumerable<string> ValidateAveragePrice(BaseTradeDomainModel trade)
    {
        var averagePriceErrorMessages = new List<string>();

        if (trade.AveragePrice == 0)
        {
            averagePriceErrorMessages.Add($"{nameof(BaseTradeDomainModel.AveragePrice)} could not be parsed.");
        }
        else if (trade.AveragePrice < 0)
        {
            averagePriceErrorMessages.Add($"{nameof(BaseTradeDomainModel.AveragePrice)} must be greater than zero.");
        }

        return averagePriceErrorMessages;
    }

    protected virtual IEnumerable<string> ValidateNotional(BaseTradeDomainModel trade)
    {
        var notionalErrorMessages = new List<string>();

        if (trade.Notional == 0)
        {
            notionalErrorMessages.Add($"{nameof(BaseTradeDomainModel.Notional)} could not be parsed.");
        }
        else if (trade.Quantity < 0)
        {
            notionalErrorMessages.Add($"{nameof(BaseTradeDomainModel.Notional)} must be greater than zero.");
        }

        return notionalErrorMessages;
    }

    protected virtual IEnumerable<string> ValidateTransactionType(BaseTradeDomainModel trade)
    {
        var transactionTypeErrorMessages = new List<string>();

        if (trade.TransactionType == TransactionType.Unknown)
        {
            transactionTypeErrorMessages.Add($"{nameof(BaseTradeDomainModel.TransactionType)} could not be parsed.");
        }

        return transactionTypeErrorMessages;
    }
}
