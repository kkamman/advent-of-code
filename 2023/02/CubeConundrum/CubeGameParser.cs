using System.Text.RegularExpressions;

namespace CubeConundrum;

public enum CubeColor
{
    Red,
    Green,
    Blue,
}

public record CubeGameResult(int GameId, List<Dictionary<CubeColor, int>> CubeReveals);

public partial class CubeGameParser
{
    public List<CubeGameResult> ParseGameResults(IEnumerable<string> gameResults)
        => gameResults.Select(ParseGameResult).ToList();

    public CubeGameResult ParseGameResult(string gameResult)
    {
        var gameId = GameIdRegex().Match(gameResult).Value;
        var cubeReveals = CubeRevealRegex().Matches(gameResult).Select(match => match.Value);

        return new CubeGameResult(
            GameId: int.TryParse(gameId, out var gameIdParsed)
                ? gameIdParsed
                : 0,
            CubeReveals: cubeReveals
                .Select(CubeRevealStringToCubeCountByColorMap)
                .ToList());
    }

    private Dictionary<CubeColor, int> CubeRevealStringToCubeCountByColorMap(string cubeReveal)
    {
        var emptyCubeCountByColorMap = Enum.GetValues(typeof(CubeColor))
            .Cast<CubeColor>()
            .ToDictionary(color => color, _ => 0);

        var cubeCountByColorMap = CubeColorCountRegex()
            .Matches(cubeReveal)
            .ToDictionary(
                match => ToCubeColor(match.Groups[2].Value),
                match => int.Parse(match.Groups[1].Value));

        return emptyCubeCountByColorMap
            .Concat(cubeCountByColorMap)
            .ToLookup(kv => kv.Key, kv => kv.Value)
            .ToDictionary(group => group.Key, group => group.Sum());
    }

    private static CubeColor ToCubeColor(string color) => color switch
    {
        "red" => CubeColor.Red,
        "green" => CubeColor.Green,
        "blue" => CubeColor.Blue,
        _ => throw new NotImplementedException()
    };

    [GeneratedRegex("(?<=^Game )(\\d+)(?=:)")]
    private static partial Regex GameIdRegex();

    [GeneratedRegex("(?<=; |: )[^;]+")]
    private static partial Regex CubeRevealRegex();

    [GeneratedRegex("(\\d+) (red|green|blue)")]
    private static partial Regex CubeColorCountRegex();
}