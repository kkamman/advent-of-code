namespace CubeConundrum;

public class CubeGamePowerCalculator
{
    public int CalculatePowerForGameResult(CubeGameResult gameResult)
        => gameResult.CubeReveals
        .SelectMany(cubeReveal => cubeReveal)
        .ToLookup(
            cubeCountByColor => cubeCountByColor.Key,
            cubeCountByColor => cubeCountByColor.Value)
        .ToDictionary(
            group => group.Key,
            group => group.Max())
        .Values
        .Aggregate((a, b) => a * b);
}
