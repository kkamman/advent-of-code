using FluentAssertions;
using Xunit;

namespace GearRatios.Test;

public class EngineSchematicParserTests
{
    private readonly string[] _stepOneExampleSchematic = new string[]
    {
        "467..114..",
        "...*......",
        "..35..633.",
        "......#...",
        "617*......",
        ".....+.58.",
        "..592.....",
        "......755.",
        "...$.*....",
        ".664.598.."
    };

    [Fact]
    public void ShouldIdentifyNumbersAdjacentToSymbolsAsPartNumbers()
    {
        // Arrange
        var tokens = new EngineSchematicLexer().LexRows(_stepOneExampleSchematic).Tokens;
        var sut = new EngineSchematicParser();

        // Act
        var engineSchematic = sut.ParseEngineSchematic(tokens);

        // Assert
        engineSchematic.PartNumbers.Should().HaveCount(8);
    }
}
