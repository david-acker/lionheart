using Lionheart.Core.DomainModels.Accounts;

namespace Lionheart.Application.Interfaces.Services.Accounts;

public interface IAccountService
{
    /// <summary>
    /// Gets the user for the provided email address.
    /// </summary>
    /// <param name="emailAddress">The email address.</param>
    Task<UserDomainModel> GetByEmail(string emailAddress);
}
