namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        class StartTagExpressionSyntax : ExpressionSyntax
        {
            public StartTagExpressionSyntax(SyntaxToken openBracket, SyntaxToken tagName, AttributeExpressionSyntax[] attributes, SyntaxToken closeBracket)
            {
                OpenBracket = openBracket;
                TagName = tagName;
                Attributes = attributes;
                CloseBracket = closeBracket;
            }

            public SyntaxToken OpenBracket { get; }
            public SyntaxToken TagName { get; }
            public AttributeExpressionSyntax[] Attributes { get; }
            public SyntaxToken CloseBracket { get; }
        }
    }
}
