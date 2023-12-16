using FluentAssertions;
using Xunit;

namespace CubeConundrum.Test;

public class CubeGameParserTests
{
    [Fact]
    public void ShouldParseGameResultStringCorrectly()
    {
        // Arrange
        var sut = new CubeGameParser();
        var gameResultString =
            "Game 72: " +
            "1 green; " +
            "1 blue, 12 green, 14 red; " +
            "3 blue, 7 green, 8 red; " +
            "12 red, 18 green; " +
            "13 green, 11 red, 1 blue; " +
            "2 blue, 6 green, 6 red";

        // Act
        var result = sut.ParseGameResult(gameResultString);

        // Assert
        result.Should().Match<CubeGameResult>(cubeGameResult =>
            cubeGameResult.GameId == 72
            && cubeGameResult.CubeReveals.Count == 6
            && cubeGameResult.CubeReveals[0][CubeColor.Red] == 0
            && cubeGameResult.CubeReveals[0][CubeColor.Green] == 1
            && cubeGameResult.CubeReveals[0][CubeColor.Blue] == 0
            && cubeGameResult.CubeReveals[1][CubeColor.Red] == 14
            && cubeGameResult.CubeReveals[1][CubeColor.Green] == 12
            && cubeGameResult.CubeReveals[1][CubeColor.Blue] == 1
            && cubeGameResult.CubeReveals[2][CubeColor.Red] == 8
            && cubeGameResult.CubeReveals[2][CubeColor.Green] == 7
            && cubeGameResult.CubeReveals[2][CubeColor.Blue] == 3
            && cubeGameResult.CubeReveals[3][CubeColor.Red] == 12
            && cubeGameResult.CubeReveals[3][CubeColor.Green] == 18
            && cubeGameResult.CubeReveals[3][CubeColor.Blue] == 0
            && cubeGameResult.CubeReveals[4][CubeColor.Red] == 11
            && cubeGameResult.CubeReveals[4][CubeColor.Green] == 13
            && cubeGameResult.CubeReveals[4][CubeColor.Blue] == 1
            && cubeGameResult.CubeReveals[5][CubeColor.Red] == 6
            && cubeGameResult.CubeReveals[5][CubeColor.Green] == 6
            && cubeGameResult.CubeReveals[5][CubeColor.Blue] == 2);
    }
}
