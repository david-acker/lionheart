namespace Lionheart.Core.DomainModels.Emails;

public class EmailRecordDomainModel
{
    /// <summary>The unique identifier for the email record.</summary>
    public int EmailRecordId { get; set; }

    /// <summary>The identifier used by the email provider's source system.</summary>
    public string SourceEmailId { get; set; }

    /// <summary>The unique identifier of the sender.</summary>
    public int SenderId { get; set; }

    /// <summary>Whether the email has already been processed.</summary>
    public bool AlreadyProcessed { get; set; }

    /// <summary>Whether the email record is marked for reprocessing.</summary>
    public bool Reprocess { get; set; }
}
