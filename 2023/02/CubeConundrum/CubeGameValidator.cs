namespace CubeConundrum;

public class CubeGameValidator
{
    private readonly IReadOnlyDictionary<CubeColor, int> _bagCubeCountByColor;

    public CubeGameValidator(Dictionary<CubeColor, int> bagCubeCountByColor)
    {
        _bagCubeCountByColor = bagCubeCountByColor;
    }

    public bool ValidateGameResult(CubeGameResult gameResult)
        => gameResult.CubeReveals
        .SelectMany(cubeReveal => cubeReveal)
        .All(countByColor => _bagCubeCountByColor[countByColor.Key] >= countByColor.Value);
}
