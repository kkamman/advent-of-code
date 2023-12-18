using System.Text.RegularExpressions;

namespace GearRatios;

public record EngineSchematicToken(string Value, int ColumnIndex, int RowIndex);

public partial class EngineSchematicLexer
{
    public IReadOnlyList<EngineSchematicToken> Tokens => _tokens;
    public int RowCount { get; private set; } = 0;

    private readonly List<EngineSchematicToken> _tokens = new();

    public EngineSchematicLexer LexRows(IEnumerable<string> engineSchematicLines)
    {
        foreach (var line in engineSchematicLines)
        {
            LexRow(line);
        }
        return this;
    }

    public EngineSchematicLexer LexRow(string engineSchematicLine)
    {
        _tokens.AddRange(EngineSchematicTokenRegex().Matches(engineSchematicLine)
            .Select(match => new EngineSchematicToken(
                Value: match.Value,
                ColumnIndex: match.Index,
                RowIndex: RowCount)));
        RowCount++;
        return this;
    }

    [GeneratedRegex("\\d+|[^\\d.]")]
    private static partial Regex EngineSchematicTokenRegex();
}
