namespace CoolHTML.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        LessThanToken,
        WhitespaceToken,
        LiteralToken,
        EndOfFileToken,
        CharacterToken,
        LessThanBackslashToken,
        DoubleQuoteToken,
        SingleQuoteToken,
        GreaterThanToken,
        EqualsToken,
        ExclamationToken,
        HyphenHyphenToken,

        // Keyword
        HtmlKeyword,
        Any,
    }
}