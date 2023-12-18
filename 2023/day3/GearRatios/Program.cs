using GearRatios;

var inputFilePath = GetInputFilePath();

var tokens = new EngineSchematicLexer().LexRows(GetEngineSchematicRows(inputFilePath)).Tokens;
var engineSchematic = new EngineSchematicParser().ParseEngineSchematic(tokens);

Console.WriteLine("Step One Answer: {0}", engineSchematic.PartNumbers.Sum());
Console.WriteLine("Step Two Answer: {0}", engineSchematic.GearRatios.Sum());

Console.ReadKey();
Environment.Exit(0);

static string GetInputFilePath()
{
    Console.WriteLine("Input file path:");
    var inputFilePath = Console.ReadLine();
    if (!File.Exists(inputFilePath))
    {
        Console.WriteLine("Invalid file path.");
        return GetInputFilePath();
    }
    return inputFilePath;
}

static IEnumerable<string> GetEngineSchematicRows(string filePath)
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