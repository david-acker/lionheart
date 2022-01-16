using Lionheart.Application.Interfaces.Repositories.Emails;
using Lionheart.Core.DomainModels.Accounts;
using Lionheart.Core.DomainModels.Emails;
using System.Data;
using System.Text.RegularExpressions;

namespace Lionheart.Application.Services.Emails;

public interface ISenderService
{
    /// <summary>
    /// Gets the sender identifier by email, creating a new
    /// sender if one does not exist.
    /// </summary>
    /// <param name="senderEmailAddress">The sender's email address.</param>
    /// <param name="transaction">The database transaction.</param>
    /// <returns>The sender identifier.</returns>
    Task<int> GetSenderId(string senderEmailAddress, IDbTransaction? transaction = null);
}

public interface ISenderVerificationService
{
    /// <summary>
    /// Verifies the email using the user's access key.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="user">The user.</param>
    /// <returns><c>true</c>, if the sender is valid;
    /// otherwise, <c>false</c>.</returns>
    bool IsVerified(EmailInputDomainModel email, UserDomainModel user);
}

public class SenderService : ISenderService, ISenderVerificationService
{
    private readonly ILionheartLogger _logger;
    private readonly ISenderRepository _senderRepository;

    public SenderService(ILionheartLogger logger,
        ISenderRepository senderRepository)
    {
        _logger = logger;
        _senderRepository = senderRepository;
    }

    public async Task<int> GetSenderId(string senderEmailAddress, IDbTransaction? transaction = null)
    {
        SenderDomainModel existingSender = await _senderRepository.GetByEmail(senderEmailAddress, transaction);

        if (existingSender != null)
        {
            return existingSender.SenderId;
        }

        return await _senderRepository.Insert(senderEmailAddress, transaction);
    }

    public bool IsVerified(EmailInputDomainModel email, UserDomainModel user)
    {
        if (string.IsNullOrWhiteSpace(user.AccessKey))
        {
            _logger.LogError($"Could not validate access key for email {email.SourceEmailId}. The access key for the user was blank.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(email.RecipientAddress))
        {
            _logger.LogError($"Could not validate access key for email {email.SourceEmailId}. The recipient address was blank.");
            return false;
        }

        Match accessKeyMatch = Regex.Match(email.RecipientAddress, @"\+([a-fA-F0-9]{32})@");
        if (!accessKeyMatch.Success)
        {
            _logger.LogInformation($"Could not validate access key for email {email.SourceEmailId}. The recipient address was incorrectly formed: {email.RecipientAddress}.");
            return false;
        }

        string accessKeyFromEmail = accessKeyMatch.Groups[1].ToString();

        return string.Equals(accessKeyFromEmail, user.AccessKey, StringComparison.OrdinalIgnoreCase);
    }
}
