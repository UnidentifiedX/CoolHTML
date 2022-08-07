namespace CoolHTML.Syntax
{
    internal sealed class EndTagExpressionSyntax : TagExpressionSyntax
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
        public override SyntaxKind Kind => SyntaxKind.EndTagExpression;
        public override TagKind TagKind => TagKind.End;
    }
}
