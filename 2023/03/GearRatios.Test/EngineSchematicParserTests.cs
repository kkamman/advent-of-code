using FluentAssertions;
using Xunit;

namespace GearRatios.Test;

public class EngineSchematicParserTests
{
    private readonly string[] _exampleSchematic = new string[]
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
        var tokens = new EngineSchematicLexer().LexRows(_exampleSchematic).Tokens;
        var sut = new EngineSchematicParser();

        // Act
        var engineSchematic = sut.ParseEngineSchematic(tokens);

        // Assert
        engineSchematic.PartNumbers.Should().HaveCount(8)
            .And.Contain(new List<int> { 467, 35, 633, 617, 592, 755, 664, 598 });
    }

    [Fact]
    public void ShouldIdentifyStarSymbolsAdjacentToTwoPartNumbersAsGearAndCalculateTheirRatio()
    {
        // Arrange
        var tokens = new EngineSchematicLexer().LexRows(_exampleSchematic).Tokens;
        var sut = new EngineSchematicParser();

        // Act
        var engineSchematic = sut.ParseEngineSchematic(tokens);

        // Assert
        engineSchematic.GearRatios.Should().HaveCount(2)
            .And.Contain(new List<int> { 16345, 451490 });
    }
}
