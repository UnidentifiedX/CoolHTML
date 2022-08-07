namespace CoolHTML.Syntax
{
    internal sealed class StartTagExpressionSyntax : TagExpressionSyntax
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
        public override SyntaxKind Kind => SyntaxKind.StartTagExpression;
        public override TagKind TagKind => TagKind.Start;
    }
}