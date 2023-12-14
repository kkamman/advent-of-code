using System.Text;

var inputFilePath = GetInputFilePath();

var result = 0;
var resultUsingSpelledOutDigits = 0;
foreach (var entry in ReadCalibrationDocument(inputFilePath))
{
    result += CalculateCalibrationValue(entry);
    resultUsingSpelledOutDigits += CalculateCalibrationValue(TransformSpelledOutDigitsToActualDigits(entry));
}

Console.WriteLine("Result: " + result);
Console.WriteLine("Result using spelled out digits: " + resultUsingSpelledOutDigits);

Console.ReadKey();
Environment.Exit(0);

static string GetInputFilePath()
{
    Console.WriteLine("Calibration document path:");
    var inputFilePath = Console.ReadLine();
    if (inputFilePath == null || !File.Exists(inputFilePath))
    {
        Console.WriteLine("Invalid input file path.");
        return GetInputFilePath();
    }
    return inputFilePath;
}

static IEnumerable<string> ReadCalibrationDocument(string filePath)
{
    using var streamReader = new StreamReader(filePath);
    while (!streamReader.EndOfStream)
    {
        var line = streamReader.ReadLine();
        if (line != null)
        {
            yield return line;
        }
    }
}

static int CalculateCalibrationValue(string text)
{
    var digits = new char[2]
    {
        text.FirstOrDefault(char.IsDigit),
        text.LastOrDefault(char.IsDigit)
    };

    return int.TryParse(digits, out int calibrationValue)
        ? calibrationValue
        : 0;
}

// Does not work, spelled out digits overlap.
static string TransformSpelledOutDigitsToActualDigits(string text)
{
    IReadOnlyDictionary<string, string> spelledOutDigitsDictionary = new Dictionary<string, string>()
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

    var stringBuilder = new StringBuilder(text);
    foreach (var (spelledOutDigit, digit) in spelledOutDigitsDictionary)
    {
        stringBuilder.Replace(spelledOutDigit, digit);
    }
    return stringBuilder.ToString();
}