namespace CoolHTML.Syntax
{
    internal sealed class TextExpressionSyntax : ExpressionSyntax
    {
        public TextExpressionSyntax(SyntaxToken text)
        {
            Text = text;
        }

        public SyntaxToken Text { get; }
        public override SyntaxKind Kind => SyntaxKind.TextExpression;
    }
}
