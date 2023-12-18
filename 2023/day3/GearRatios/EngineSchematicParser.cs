namespace GearRatios;

public record EngineSchematic(IReadOnlyList<int> PartNumbers);

public class EngineSchematicParser
{
    public EngineSchematic ParseEngineSchematic(IEnumerable<EngineSchematicToken> tokens)
    {
        var numberTokens = tokens.Where(token => token.Value.All(char.IsDigit));
        var symbolTokens = tokens.Except(numberTokens);

        var partNumbers = numberTokens
            .Where(numberToken => symbolTokens.Any(symbolToken => AreTokensAdjacent(numberToken, symbolToken)))
            .Select(numberToken => int.Parse(numberToken.Value))
            .ToList();

        return new EngineSchematic(partNumbers);
    }

    private static bool AreTokensAdjacent(EngineSchematicToken tokenA, EngineSchematicToken tokenB)
    {
        if (tokenB.RowIndex == tokenA.RowIndex)
        {
            return tokenB.ColumnIndex == tokenA.ColumnIndex - 1
                || tokenB.ColumnIndex == tokenA.ColumnIndex + tokenA.Value.Length;
        }
        else if (tokenB.RowIndex == tokenA.RowIndex - 1 || tokenB.RowIndex == tokenA.RowIndex + 1)
        {
            return tokenB.ColumnIndex >= tokenA.ColumnIndex - 1
                && tokenB.ColumnIndex <= tokenA.ColumnIndex + tokenA.Value.Length;
        }

        return false;
    }
}
