namespace CoolHTML.Syntax
{
    internal abstract class TagExpressionSyntax : ExpressionSyntax
    {
        public abstract TagKind TagKind { get; }
    }
}
