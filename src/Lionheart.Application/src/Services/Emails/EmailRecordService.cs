using Lionheart.Application.Interfaces.Repositories;
using Lionheart.Application.Interfaces.Repositories.Emails;
using Lionheart.Core.DomainModels.Emails;
using System.Data;

namespace Lionheart.Application.Services.Emails;

public interface IEmailRecordService
{
    /// <summary>
    /// Creates an email record for the email.
    /// </summary>
    /// <param name="email">The email.</param>
    Task<EmailRecordDomainModel> Create(EmailInputDomainModel email);
}

public class EmailRecordService : IEmailRecordService
{
    private readonly IEmailRecordRepository _emailRecordRepository;
    private readonly ISenderService _writableSenderService;
    private readonly IUnitOfWork _unitOfWork;

    public EmailRecordService(
        IEmailRecordRepository emailRecordRepository,
         ISenderService writableSenderService,
         IUnitOfWork unitOfWork)
    {
        _emailRecordRepository = emailRecordRepository;
        _writableSenderService = writableSenderService;
        _unitOfWork = unitOfWork;
    }

    public async Task<EmailRecordDomainModel> Create(EmailInputDomainModel email)
    {
        EmailRecordDomainModel emailRecord = 
            await _emailRecordRepository.GetBySourceEmailId(email.SourceEmailId);

        if (emailRecord != null)
        {
            emailRecord.AlreadyProcessed = true;

            return emailRecord;
        }

        using IDbTransaction transaction = _unitOfWork.StartTransaction();

        int senderId = await _writableSenderService.GetSenderId(email.SenderAddress, transaction);

        emailRecord = new EmailRecordDomainModel
        {
            SourceEmailId = email.SourceEmailId,
            SenderId = senderId
        };

        emailRecord.EmailRecordId = await _emailRecordRepository.Insert(emailRecord, transaction);

        _unitOfWork.CommitTransaction();

        return emailRecord;
    }
}
