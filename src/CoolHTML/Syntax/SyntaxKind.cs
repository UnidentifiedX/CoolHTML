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

        // Keywords
        HtmlKeyword,
        Any,

        // Expressions 
        AttributeExpression,
        EndOfFileExpression,
        EndTagExpression,
        StartTagExpression,
        StringExpression,
        TextExpression
    }
}