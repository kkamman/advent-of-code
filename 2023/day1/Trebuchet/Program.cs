var inputFilePath = GetInputFilePath();

var result = 0;
foreach (var entry in ReadCalibrationDocument(inputFilePath))
{
    result += CalculateCalibrationValue(entry);
}

Console.WriteLine("Result: " + result);

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
    var result = string.Empty;
    char? lastSeenDigit = null;
    foreach (var character in text)
    {
        if (char.IsDigit(character))
        {
            if (lastSeenDigit == null)
            {
                result += character;
            }
            lastSeenDigit = character;
        }
    }
    return int.Parse(result + lastSeenDigit);
}