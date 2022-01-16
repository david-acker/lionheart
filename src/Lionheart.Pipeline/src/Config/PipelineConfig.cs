namespace Lionheart.Pipeline.Config;

public class PipelineConfig
{
    /// <summary>Whether to skip emails with no associated user.</summary>
    public bool SkipUserlessEmails { get; set; } = true;
}
