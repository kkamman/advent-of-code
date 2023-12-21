using CubeConundrum;
using Utils;

ConsoleUtils.RunProgram(
    title: "--- Day 2: Cube Conundrum ---",
    inputFilePath =>
    {
        var parsedGameResults = new CubeGameParser()
            .ParseGameResults(File.ReadLines(inputFilePath));

        var validator = new CubeGameValidator(bagCubeCountByColor: new()
        {
            { CubeColor.Red, 12 },
            { CubeColor.Green, 13 },
            { CubeColor.Blue, 14 },
        });

        var stepOneAnswer = parsedGameResults
            .Where(validator.ValidateGameResult)
            .Sum(game => game.GameId);

        var stepTwoAnswer = parsedGameResults
            .Select(new CubeGamePowerCalculator().CalculatePowerForGameResult)
            .Sum();

        return $"Step One = {stepOneAnswer}; Step Two = {stepTwoAnswer};";
    });