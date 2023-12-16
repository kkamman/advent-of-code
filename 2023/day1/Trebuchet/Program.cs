using Trebuchet;

var inputFilePath = GetCalibrationDocumentFilePath();
var useSpelledOutDigits = GetUseSpelledOutDigitSetting();

var document = GetCalibrationDocument(inputFilePath);
var parser = new CalibrationDocumentParser(useSpelledOutDigits);

parser.ParseDocument(document);

Console.WriteLine("Result: {0}", parser.GetResult());

Console.ReadKey();
Environment.Exit(0);

static string GetCalibrationDocumentFilePath()
{
    Console.WriteLine("Calibration document file path:");
    var inputFilePath = Console.ReadLine();
    if (!File.Exists(inputFilePath))
    {
        Console.WriteLine("Invalid calibration document file path.");
        return GetCalibrationDocumentFilePath();
    }
    return inputFilePath;
}

static bool GetUseSpelledOutDigitSetting()
{
    Console.WriteLine("Use spelled out digits? [y/n]");
    var answer = Console.ReadLine();

    if (answer == "y")
    {
        return true;
    }

    if (answer == "n")
    {
        return false;
    }

    return GetUseSpelledOutDigitSetting();
}

static IEnumerable<string> GetCalibrationDocument(string filePath)
{
    using var streamReader = new StreamReader(filePath);
    while (!streamReader.EndOfStream)
    {
        var entry = streamReader.ReadLine();
        if (entry != null)
        {
            yield return entry;
        }
    }
}