namespace Lionheart.Core.DomainModels.Accounts;

public class UserDomainModel
{
    /// <summary>The unique identifier for the user.</summary>
    public int UserId { get; set; }

    /// <summary>The email address for the user.</summary>
    public string Email { get; set; }

    /// <summary>The access key for the user.</summary>
    /// <remarks>
    /// Used to verify the identify of trade confirmation
    /// emails claiming to be sent from user's email address,
    /// in order to prevent spoofing.
    /// </remarks>
    public string AccessKey { get; set; }
}
