namespace Trebuchet;

public class CalibrationDocumentParser
{
    private readonly bool _useSpelledOutDigits;
    private readonly IReadOnlyDictionary<string, string> _spelledOutDigitToActualDigitMap =
        new Dictionary<string, string>
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };

    private int _result = 0;

    public CalibrationDocumentParser(bool useSpelledOutDigits = false)
    {
        _useSpelledOutDigits = useSpelledOutDigits;
    }

    public void ParseDocument(IEnumerable<string> entries)
    {
        foreach (var entry in entries)
        {
            ParseEntry(entry);
        }
    }

    public void ParseEntry(string entry)
    {
        var stringsToSearchFor = _spelledOutDigitToActualDigitMap.Values;

        if (_useSpelledOutDigits)
        {
            stringsToSearchFor = stringsToSearchFor.Concat(_spelledOutDigitToActualDigitMap.Keys);
        }

        string? firstDigit = null;
        string? lastDigit = null;
        for (int i = 1; i <= entry.Length && (firstDigit == null || lastDigit == null); i++)
        {
            firstDigit ??= stringsToSearchFor.FirstOrDefault(entry[..i].Contains);
            lastDigit ??= stringsToSearchFor.FirstOrDefault(entry[^i..].Contains);
        }

        if (firstDigit != null && lastDigit != null)
        {
            var actualDigits = new string[] { firstDigit, lastDigit }
                .Select(digit => digit.Length == 1 ? digit[0] : _spelledOutDigitToActualDigitMap[digit][0])
                .ToArray();

            if (int.TryParse(actualDigits, out int calibrationValue))
            {
                _result += calibrationValue;
            }
        }
    }

    public int GetResult() => _result;
}
