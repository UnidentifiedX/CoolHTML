namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        class EndTagExpressionSyntax : ExpressionSyntax
        {
            public EndTagExpressionSyntax(SyntaxToken openBracketSlash, SyntaxToken tagName, SyntaxToken closeBracket)
            {
                OpenBracketSlash = openBracketSlash;
                TagName = tagName;
                CloseBracket = closeBracket;
            }

            public SyntaxToken OpenBracketSlash { get; }
            public SyntaxToken TagName { get; }
            public SyntaxToken CloseBracket { get; }
        }
    }
}
