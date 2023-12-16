using FluentAssertions;
using Xunit;

namespace CubeConundrum.Test;

public class CubeGamePowerCalculatorTests
{
    [Theory]
    [ClassData(typeof(StepTwoTestData))]
    public void ShouldValidateCubeGameResultCorrectly(
        CubeGameResult gameResult,
        int expectedPower)
    {
        // Arrange
        var sut = new CubeGamePowerCalculator();

        // Act
        var power = sut.CalculatePowerForGameResult(gameResult);

        // Assert
        power.Should().Be(expectedPower);
    }

    public class StepTwoTestData : TheoryData<CubeGameResult, int>
    {
        public StepTwoTestData()
        {
            var parser = new CubeGameParser();
            Add(parser.ParseGameResult("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"), 48);
            Add(parser.ParseGameResult("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"), 12);
            Add(parser.ParseGameResult("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"), 1560);
            Add(parser.ParseGameResult("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"), 630);
            Add(parser.ParseGameResult("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"), 36);
        }
    }
}
