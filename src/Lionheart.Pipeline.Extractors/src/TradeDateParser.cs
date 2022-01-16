using System.Globalization;
using System.Text.RegularExpressions;

namespace Lionheart.Pipeline.Extractors;

internal static class TradeDateParser
{
    /// <summary>
    /// Attempts to convert the string representation of the trade date to a <c>DateTime</c>.
    /// </summary>
    /// <param name="tradeDateInput">The string representation of the trade date.</param>
    /// <returns>
    /// The resulting <c>DateTime</c>; otherwise, <c>DateTime.MinValue</c>.
    /// </returns>
    public static DateTime Parse(string? tradeDateInput)
    {
        if (string.IsNullOrEmpty(tradeDateInput))
        {
            return DateTime.MinValue;
        }

        // Remove ordinal indicator.
        var regex = new Regex("([0-9]{1,2})(?:st|nd|rd|th)(, )");
        tradeDateInput = regex.Replace(tradeDateInput, "$1$2");

        tradeDateInput = tradeDateInput.Replace(" at ", " ").Replace(",", "");

        bool success = DateTime.TryParseExact(tradeDateInput,
            "MMMM d yyyy h:mm tt",
            CultureInfo.CurrentCulture,
            DateTimeStyles.None,
            out DateTime tradeDate);

        return success ? tradeDate : DateTime.MinValue;
    }
}
