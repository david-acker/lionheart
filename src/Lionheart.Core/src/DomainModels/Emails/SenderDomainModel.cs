namespace Lionheart.Core.DomainModels.Emails;

public class SenderDomainModel
{
    /// <summary>The unique identifier for the sender.</summary>
    public int SenderId { get; set; }

    /// <summary>The email address associated with the sender.</summary>
    public string Email { get; set; }
}
