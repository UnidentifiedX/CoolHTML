namespace CoolHTML.Syntax
{
    internal sealed class AttributeExpressionSyntax : ExpressionSyntax
    {
        public AttributeExpressionSyntax(SyntaxToken attributeName, SyntaxToken equalSign, StringExpressionSyntax value)
        {
            AttributeName = attributeName;
            EqualSign = equalSign;
            Value = value;
        }

        public SyntaxToken AttributeName { get; }
        public SyntaxToken EqualSign { get; }
        public StringExpressionSyntax Value { get; }
        public override SyntaxKind Kind => SyntaxKind.AttributeExpression;
    }
}
