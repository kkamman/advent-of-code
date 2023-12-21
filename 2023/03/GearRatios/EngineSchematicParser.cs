namespace GearRatios;

public record EngineSchematic(IReadOnlyList<int> PartNumbers, IReadOnlyList<int> GearRatios);

public class EngineSchematicParser
{
    public EngineSchematic ParseEngineSchematic(IEnumerable<EngineSchematicToken> tokens)
    {
        var partNumberTokens = new List<EngineSchematicToken>();
        var possibleGearTokens = new List<TokenWithAdjacentTokens>();

        foreach (var tokenWithAdjacentTokens in GetTokensWithAdjacentTokens(tokens))
        {
            var (token, adjacentTokens) = tokenWithAdjacentTokens;

            if (token is NumberToken numberToken
                && adjacentTokens.OfType<SymbolToken>().Any())
            {
                partNumberTokens.Add(numberToken);
            }

            if (token is SymbolToken symbolToken
                && symbolToken.Value == "*"
                && adjacentTokens.Count > 1)
            {
                possibleGearTokens.Add(tokenWithAdjacentTokens);
            }
        }

        var gearTokens = possibleGearTokens
            .Select(token => token.AdjacentTokens
                .Where(adjacentToken => partNumberTokens.Contains(adjacentToken))
                .ToList())
            .Where(adjacentPartNumberTokens => adjacentPartNumberTokens.Count == 2);

        return new EngineSchematic(
            PartNumbers: partNumberTokens
                .Select(token => int.Parse(token.Value))
                .ToList(),
            GearRatios: gearTokens
                .Select(adjacentPartNumberTokens => adjacentPartNumberTokens
                    .Select(partNumberToken => int.Parse(partNumberToken.Value))
                    .Aggregate((a, b) => a * b))
                .ToList());
    }

    private record TokenWithAdjacentTokens(EngineSchematicToken Token, IReadOnlyList<EngineSchematicToken> AdjacentTokens);

    private static IEnumerable<TokenWithAdjacentTokens> GetTokensWithAdjacentTokens(IEnumerable<EngineSchematicToken> tokens)
    {
        var tokensByRow = tokens.ToLookup(token => token.RowIndex);

        foreach (var rowIndex in tokensByRow.Select(group => group.Key))
        {
            var tokensToCompareForAdjacency = new int[] { rowIndex - 1, rowIndex, rowIndex + 1 }
                .SelectMany(rowIndex => tokensByRow[rowIndex])
                .ToList();

            foreach (var token in tokensByRow[rowIndex])
            {
                var adjacentTokens = tokensToCompareForAdjacency
                    .Where(tokenToCompare => AreTokensAdjacent(token, tokenToCompare))
                    .ToList();

                yield return new TokenWithAdjacentTokens(token, adjacentTokens);
            }
        }
    }

    private static bool AreTokensAdjacent(EngineSchematicToken tokenA, EngineSchematicToken tokenB)
    {
        if (tokenA == tokenB)
        {
            return false;
        }

        var maxStart = Math.Max(tokenA.ColumnIndex - 1, tokenB.ColumnIndex);
        var minEnd = Math.Min(tokenA.ColumnIndex + tokenA.Value.Length, tokenB.ColumnIndex + tokenB.Value.Length - 1);

        return maxStart <= minEnd;
    }
}
