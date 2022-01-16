namespace Lionheart.Core.DomainModels.Emails;

public class EmailInputDomainModel
{
    /// <summary>The identifier used by the email provider's source system.</summary>
    public string SourceEmailId { get; set; }

    /// <summary>The email address of the sender.</summary>
    public string SenderAddress { get; set; }

    /// <summary>The email address of the recipient.</summary>
    public string RecipientAddress { get; set; }

    /// <summary>The email body.</summary>
    public string Body { get; set; }
}
