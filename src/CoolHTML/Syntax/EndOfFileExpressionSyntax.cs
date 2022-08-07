namespace CoolHTML.Syntax
{
    internal sealed class EndOfFileExpressionSyntax : ExpressionSyntax
    {
        public EndOfFileExpressionSyntax(SyntaxToken endOfFileToken)
        {
            EndOfFileToken = endOfFileToken;
        }

        public SyntaxToken EndOfFileToken { get; }
        public override SyntaxKind Kind => SyntaxKind.EndOfFileExpression;
    } 
}
