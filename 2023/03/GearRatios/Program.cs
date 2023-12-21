using GearRatios;
using Utils;

ConsoleUtils.RunProgram(
    title: "--- Day 3: Gear Ratios ---",
    inputFilePath =>
    {
        var tokens = new EngineSchematicLexer().LexRows(File.ReadLines(inputFilePath)).Tokens;
        var engineSchematic = new EngineSchematicParser().ParseEngineSchematic(tokens);
        return $"Step One = {engineSchematic.PartNumbers.Sum()}; Step Two = {engineSchematic.GearRatios.Sum()};";
    });