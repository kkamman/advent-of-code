using FluentAssertions;
using Xunit;

namespace GearRatios.Test;

public class EngineSchematicLexerTests
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
    public void ShouldReturnEachIndividualSymbolAsToken()
    {
        // Arrange
        var sut = new EngineSchematicLexer();

        // Act
        sut.LexRows(_stepOneExampleSchematic);

        // Assert
        sut.Tokens.Should().HaveElementAt(index: 2, new EngineSchematicToken(Value: "*", ColumnIndex: 3, RowIndex: 1))
            .And.HaveElementAt(index: 5, new EngineSchematicToken(Value: "#", ColumnIndex: 6, RowIndex: 3))
            .And.HaveElementAt(index: 7, new EngineSchematicToken(Value: "*", ColumnIndex: 3, RowIndex: 4))
            .And.HaveElementAt(index: 8, new EngineSchematicToken(Value: "+", ColumnIndex: 5, RowIndex: 5))
            .And.HaveElementAt(index: 12, new EngineSchematicToken(Value: "$", ColumnIndex: 3, RowIndex: 8))
            .And.HaveElementAt(index: 13, new EngineSchematicToken(Value: "*", ColumnIndex: 5, RowIndex: 8));
    }

    [Fact]
    public void ShouldReturnConsecutiveDigitsAsToken()
    {
        // Arrange
        var sut = new EngineSchematicLexer();

        // Act
        sut.LexRows(_stepOneExampleSchematic);

        // Assert
        sut.Tokens.Should().HaveElementAt(index: 0, new EngineSchematicToken(Value: "467", ColumnIndex: 0, RowIndex: 0))
            .And.HaveElementAt(index: 1, new EngineSchematicToken(Value: "114", ColumnIndex: 5, RowIndex: 0))
            .And.HaveElementAt(index: 3, new EngineSchematicToken(Value: "35", ColumnIndex: 2, RowIndex: 2))
            .And.HaveElementAt(index: 4, new EngineSchematicToken(Value: "633", ColumnIndex: 6, RowIndex: 2))
            .And.HaveElementAt(index: 6, new EngineSchematicToken(Value: "617", ColumnIndex: 0, RowIndex: 4))
            .And.HaveElementAt(index: 9, new EngineSchematicToken(Value: "58", ColumnIndex: 7, RowIndex: 5))
            .And.HaveElementAt(index: 10, new EngineSchematicToken(Value: "592", ColumnIndex: 2, RowIndex: 6))
            .And.HaveElementAt(index: 11, new EngineSchematicToken(Value: "755", ColumnIndex: 6, RowIndex: 7))
            .And.HaveElementAt(index: 14, new EngineSchematicToken(Value: "664", ColumnIndex: 1, RowIndex: 9))
            .And.HaveElementAt(index: 15, new EngineSchematicToken(Value: "598", ColumnIndex: 5, RowIndex: 9));
    }
}