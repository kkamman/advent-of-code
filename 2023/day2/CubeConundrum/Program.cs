using CubeConundrum;

var inputFilePath = GetInputFilePath();
var gameResults = GetGameResultsFromFile(inputFilePath);

var parser = new CubeGameParser();
var parsedGameResults = parser.ParseGameResults(gameResults);

var validator = new CubeGameValidator(bagCubeCountByColor: new()
{
    { CubeColor.Red, 12 },
    { CubeColor.Green, 13 },
    { CubeColor.Blue, 14 },
});

var validGameIds = parsedGameResults.Where(validator.ValidateGameResult).Select(game => game.GameId);
var stepOneAnswer = validGameIds.Sum();

var powerCalculator = new CubeGamePowerCalculator();
var stepTwoAnswer = parsedGameResults.Select(powerCalculator.CalculatePowerForGameResult).Sum();

Console.WriteLine("Step One Answer: {0}", stepOneAnswer);
Console.WriteLine("Step Two Answer: {0}", stepTwoAnswer);

Console.ReadKey();
Environment.Exit(0);

static string GetInputFilePath()
{
    Console.WriteLine("Input file path:");
    var inputFilePath = Console.ReadLine();
    if (!File.Exists(inputFilePath))
    {
        Console.WriteLine("Inputfile path.");
        return GetInputFilePath();
    }
    return inputFilePath;
}

static IEnumerable<string> GetGameResultsFromFile(string filePath)
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