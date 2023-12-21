using FluentAssertions;
using Xunit;

namespace Trebuchet.Test;

public class CalibrationDocumentParserTests
{
    [Theory]
    [ClassData(typeof(StepOneTestData))]
    public void ShouldCaptureFirstAndLastDigit(string entry, int expectedResult)
    {
        // Arrange
        var sut = new CalibrationDocumentParser(useSpelledOutDigits: false);

        // Act
        sut.ParseEntry(entry);

        // Assert
        sut.Result.Should().Be(expectedResult);
    }

    public class StepOneTestData : TheoryData<string, int>
    {
        public StepOneTestData()
        {
            Add("1abc2", 12);
            Add("pqr3stu8vwx", 38);
            Add("a1b2c3d4e5f", 15);
            Add("treb7uchet", 77);
        }
    }

    [Theory]
    [ClassData(typeof(StepTwoTestData))]
    public void ShouldCaptureFirstAndLastDigitOrSpelledOutDigit(string entry, int expectedResult)
    {
        // Arrange
        var sut = new CalibrationDocumentParser(useSpelledOutDigits: true);

        // Act
        sut.ParseEntry(entry);

        // Assert
        sut.Result.Should().Be(expectedResult);
    }

    public class StepTwoTestData : TheoryData<string, int>
    {
        public StepTwoTestData()
        {
            Add("two1nine", 29);
            Add("eightwothree", 83);
            Add("abcone2threexyz", 13);
            Add("xtwone3four", 24);
            Add("4nineeightseven2", 42);
            Add("zoneight234", 14);
            Add("7pqrstsixteen", 76);
        }
    }
}