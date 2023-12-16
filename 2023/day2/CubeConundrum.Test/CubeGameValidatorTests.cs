using FluentAssertions;
using Xunit;

namespace CubeConundrum.Test;

public class CubeGameValidatorTests
{
    [Theory]
    [ClassData(typeof(StepOneTestData))]
    public void ShouldValidateCubeGameResultCorrectly(
        CubeGameResult gameResult,
        Dictionary<CubeColor, int> bagContents,
        bool expectedValidationResult)
    {
        // Arrange
        var sut = new CubeGameValidator(bagContents);

        // Act
        var validationResult = sut.ValidateGameResult(gameResult);

        // Assert
        validationResult.Should().Be(expectedValidationResult);
    }

    public class StepOneTestData : TheoryData<CubeGameResult, Dictionary<CubeColor, int>, bool>
    {
        public StepOneTestData()
        {
            var parser = new CubeGameParser();
            var bagContents = new Dictionary<CubeColor, int>
            {
                { CubeColor.Red, 12 },
                { CubeColor.Green, 13 },
                { CubeColor.Blue, 14 }
            };
            Add(parser.ParseGameResult("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"), bagContents, true);
            Add(parser.ParseGameResult("Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue"), bagContents, true);
            Add(parser.ParseGameResult("Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red"), bagContents, false);
            Add(parser.ParseGameResult("Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red"), bagContents, false);
            Add(parser.ParseGameResult("Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"), bagContents, true);
        }
    }
}