namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        internal class TextExpressionSyntax : ExpressionSyntax
        {
            public TextExpressionSyntax(SyntaxToken text)
            {
                Text = text;
            }

            public SyntaxToken Text { get; }
        }
    }
}
