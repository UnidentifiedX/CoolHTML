namespace CoolHTML.Syntax
{
    internal sealed class StringExpressionSyntax : ExpressionSyntax
    {
        public StringExpressionSyntax(SyntaxToken openQuote, SyntaxToken[] stringContent, SyntaxToken closeQuote)
        {
            OpenQuote = openQuote;
            StringContent = stringContent;
            CloseQuote = closeQuote;
        }

        public SyntaxToken OpenQuote { get; }
        public SyntaxToken[] StringContent { get; }
        public SyntaxToken CloseQuote { get; }
        public override SyntaxKind Kind => SyntaxKind.StringExpression;
    }
}
