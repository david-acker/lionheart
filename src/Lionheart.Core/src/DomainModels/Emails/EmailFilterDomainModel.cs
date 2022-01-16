namespace Lionheart.Core.DomainModels.Emails;

public class EmailFilterDomainModel
{
    /// <summary>The query string used when requesting emails from the email provider.</summary>
    public string Query { get; set; }
}
