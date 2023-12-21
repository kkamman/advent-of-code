using System.Text.RegularExpressions;

namespace GearRatios;

public abstract record EngineSchematicToken(string Value, int ColumnIndex, int RowIndex);

public record NumberToken(string Value, int ColumnIndex, int RowIndex)
    : EngineSchematicToken(Value, ColumnIndex, RowIndex);

public record SymbolToken(string Value, int ColumnIndex, int RowIndex)
    : EngineSchematicToken(Value, ColumnIndex, RowIndex);

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
        _tokens.AddRange(EngineSchematicTokenRegex()
            .Matches(engineSchematicLine)
            .Select(RegexMatchToToken));
        RowCount++;
        return this;
    }

    private EngineSchematicToken RegexMatchToToken(Match match)
    {
        return match.Value.All(char.IsDigit)
            ? new NumberToken(
                Value: match.Value,
                ColumnIndex: match.Index,
                RowIndex: RowCount)
            : new SymbolToken(
                Value: match.Value,
                ColumnIndex: match.Index,
                RowIndex: RowCount);
    }

    [GeneratedRegex("\\d+|[^\\d.]")]
    private static partial Regex EngineSchematicTokenRegex();
}
