using Lionheart.Application.Interfaces.Services.Trades;
using Lionheart.Application.Interfaces.Services;
using Lionheart.Application.Services.Emails;
using Lionheart.Application.Services.Trades;
using Lionheart.Core.DomainModels.Accounts;
using Lionheart.Core.DomainModels.Emails;
using Lionheart.Core.DomainModels.Trades;
using Lionheart.Application;
using Lionheart.Pipeline.Extraction;
using Lionheart.Pipeline.Config;
using Lionheart.Application.Interfaces.Services.Accounts;

namespace Lionheart.Pipeline.Processors;

public interface ITradeProcessor<TTrade> where TTrade : class, IBaseTrade
{
    /// <summary>
    /// Adds the email and any resulting trades to the system.
    /// </summary>
    /// <param name="email">The email.</param>
    Task Process(EmailInputDomainModel email);
}

public class TradeProcessor<TTrade> : ITradeProcessor<TTrade> where TTrade : class, IBaseTrade
{
    protected readonly ILionheartLogger _logger;
    protected readonly IEmailRecordService _emailRecordService;
    protected readonly ISenderVerificationService _senderVerificationService;
    protected readonly ITradeExtractionEngine<TTrade> _tradeExtractionEngine;
    protected readonly ITradeService<TTrade> _tradeService;
    protected readonly ITradeToEmailRecordService _tradeToEmailRecordService;
    protected readonly IValidationService<TTrade> _tradeValidationService;
    protected readonly IAccountService _accountService;

    private readonly PipelineConfig _pipelineConfig;

    public TradeProcessor(ILionheartLogger logger,
        IEmailRecordService emailRecordService,
        ISenderVerificationService senderVerificationService,
        ITradeExtractionEngine<TTrade> tradeExtractionEngine,
        ITradeService<TTrade> tradeService,
        ITradeToEmailRecordService tradeToEmailRecordService,
        IValidationService<TTrade> tradeValidationService,
        IAccountService accountService,

        PipelineConfig pipelineConfig)
    {
        _logger = logger;
        _emailRecordService = emailRecordService;
        _senderVerificationService = senderVerificationService;
        _tradeExtractionEngine = tradeExtractionEngine;
        _tradeService = tradeService;
        _tradeToEmailRecordService = tradeToEmailRecordService;
        _tradeValidationService = tradeValidationService;
        _accountService = accountService;

        _pipelineConfig = pipelineConfig;
    }

    public async Task Process(EmailInputDomainModel email)
    {
        EmailRecordDomainModel emailRecord = await _emailRecordService.Create(email);

        if (emailRecord.AlreadyProcessed && !emailRecord.Reprocess)
        {
            _logger.LogInformation($"Email {email.SourceEmailId} has already been processed. Skipping.");
            return;
        }

        UserDomainModel user = await _accountService.GetByEmail(email.SenderAddress);

        if (user == null)
        {
            if (_pipelineConfig.SkipUserlessEmails)
            {
                _logger.LogInformation($"Email {email.SourceEmailId} has no associated user. Skipping.");
                return;
            }
        }
        else
        {
            if (!_senderVerificationService.IsVerified(email, user))
            {
                _logger.LogInformation($"Email {email.SourceEmailId} has an invalid sender. Skipping.");
                return;
            }
        }

        TTrade? trade = _tradeExtractionEngine.Extract(email);
        if (trade == null)
        {
            _logger.LogWarning($"Failed to extract a trade from email {email.SourceEmailId}.");
            return;
        }
        else if (!_tradeValidationService.IsValid(trade))
        {
            _logger.LogWarning($"The trade created from email {email.SourceEmailId} was not valid.");
            return;
        }

        trade.UserId = user?.UserId;
        int tradeId = await _tradeService.Insert(trade);

        await _tradeToEmailRecordService.Link(tradeId, emailRecord.EmailRecordId);
    }
}
