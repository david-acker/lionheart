using Microsoft.Extensions.Logging;

namespace Lionheart.Application;

public interface ILionheartLogger
{
    void LogTrace(string message);
    void LogDebug(string message);
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message);
    void LogCritical(string message);
}

public class LionheartLogger : ILionheartLogger
{
    private readonly ILogger _logger;

    public LionheartLogger(ILogger logger)
    {
        _logger = logger;
    }

#pragma warning disable CA2254 // Template should be a static expression
    public void LogTrace(string message) => _logger.LogTrace(message);

    public void LogDebug(string message) => _logger.LogDebug(message);

    public void LogInformation(string message) => _logger.LogInformation(message);

    public void LogWarning(string message) => _logger.LogWarning(message);

    public void LogError(string message) => _logger.LogError(message);

    public void LogCritical(string message) => _logger.LogCritical(message);

#pragma warning restore CA2254 // Template should be a static expression
}
