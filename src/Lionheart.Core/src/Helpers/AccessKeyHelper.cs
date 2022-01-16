namespace Lionheart.Core.Helpers;

public static class AccessKeyHelper
{
    /// <summary>
    /// Generates a new GUID-based access key.
    /// </summary>
    /// <returns></returns>
    public static string Generate() => Guid.NewGuid().ToString().Replace("-", "").ToUpper();
}

