namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        sealed class AttributeExpressionSyntax : ExpressionSyntax
        {
            public AttributeExpressionSyntax(SyntaxToken attributeName, SyntaxToken equalSign, StringExpressionSyntax attribute)
            {
                AttributeName = attributeName;
                EqualSign = equalSign;
                Attribute = attribute;
            }

            public SyntaxToken AttributeName { get; }
            public SyntaxToken EqualSign { get; }
            public StringExpressionSyntax Attribute { get; }
        }
    }
}
