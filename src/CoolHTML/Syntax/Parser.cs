using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CoolHTML;

namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        private List<CoolHTMLNode> _nodes;

        public Parser(string html)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer(html);
            SyntaxToken token;

            do
            {
                token = lexer.Lex();

                if(token.Kind != SyntaxKind.WhitespaceToken)
                    tokens.Add(token);
            } while (token.Kind != SyntaxKind.EndOfFileToken);

            _tokens = tokens.ToArray();

            //foreach (var item in _tokens)
            //{
            //    Console.WriteLine(item.Kind + ": " + item.Text);
            //}
        }

        public List<CoolHTMLNode> Parse()
        {
            var expressions = new List<ExpressionSyntax>();
            ExpressionSyntax expression;

            do
            {
                expression = ParseExpression();
                expressions.Add(expression);
                Console.WriteLine(expression.ToString());
            } while (expression.Kind != SyntaxKind.EndOfFileExpression);

            return ParseTree(expressions);
        }

        private List<CoolHTMLNode> ParseTree(List<ExpressionSyntax> expressions)
        {
            ParseElements(expressions);

            Debugger.Break();
            return _nodes;
        }

        private void ParseElements(List<ExpressionSyntax> expressions, CoolHTMLNode parentNode = null)
        {
            var _parentNode = parentNode;
            for (int i = 0; i < expressions.Count; i++)
            {
                var expression = expressions[i];

                if (expression.Kind == SyntaxKind.StartTagExpression)
                {
                    var startTagExpression = (StartTagExpressionSyntax)expression;
                    var attributeArray = startTagExpression.Attributes;
                    var attributeList = new List<CoolHTMLAttribute>();

                    foreach (var attribute in attributeArray)
                    {
                        var stringContent = attribute.Value.StringContent;
                        var content = "";

                        foreach (var str in stringContent)
                        {
                            content += str.Text;
                        }

                        attributeList.Add(
                                new CoolHTMLAttribute(attribute.AttributeName.Text, content)
                            );
                    }

                    var children = new List<CoolHTMLNode>();

                    if (_parentNode != null)
                        children = _parentNode.Children;

                    var node = new CoolHTMLNode(_parentNode,
                        children,
                        SwitchType(startTagExpression.TagName.Text),
                        attributeList,
                        null);

                    if(_parentNode != null)
                        _parentNode.Children.Add(node);

                    _parentNode = node;

                    expressions.RemoveAt(0);
                    ParseElements(expressions, _parentNode);
                }
                else if (expression.Kind == SyntaxKind.EndTagExpression)
                {
                    if(_parentNode.Parent == null)
                    {
                        _nodes = new List<CoolHTMLNode>() { _parentNode };
                        return;
                    }                     

                    _parentNode = _parentNode.Parent;
                    expressions.RemoveAt(0);
                    ParseElements(expressions, _parentNode);
                }
                else if (expression.Kind == SyntaxKind.TextExpression)
                {
                    var textExpression = (TextExpressionSyntax)expression;
                    _parentNode.TextContent = new CoolHTMLTextContent(textExpression.Text.Text);
                    
                    expressions.RemoveAt(0);
                    ParseElements(expressions, _parentNode);
                }
            }
        }

        private static List<CoolHTMLAttribute> GetAttributeList(AttributeExpressionSyntax[] attributeArray)
        {
            var attributes = new List<CoolHTMLAttribute>();
            foreach (var attribute in attributeArray)
            {
                string attributeValue = "";
                foreach (var value in attribute.Value.StringContent)
                {
                    attributeValue += value.Text;
                }

                var coolHTMLAttribute = new CoolHTMLAttribute(attribute.AttributeName.Text, attributeValue);
                attributes.Add(coolHTMLAttribute);
            }

            return attributes;
        }

        private ExpressionSyntax ParseExpression()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.EndOfFileToken:
                    var endOfFileToken = Match(SyntaxKind.CharacterToken);
                    return new EndOfFileExpressionSyntax(endOfFileToken);
                case SyntaxKind.LessThanToken:
                    return ParseStartTag();
                case SyntaxKind.LessThanBackslashToken:
                    return ParseEndTag();
                default:
                    return ParseText();
            }
        }

        private TagType GetType(ExpressionSyntax expression)
        {
            var tagType = ((TagExpressionSyntax)expression).TagKind;
            return SwitchType(((StartTagExpressionSyntax)expression).TagName.Text);
        }

        private TagType SwitchType(string text)
        {
            switch (text.ToLower())
            {
                case "body":
                    return TagType.BodyTag;
                case "head":
                    return TagType.HeadTag;
                case "html":
                    return TagType.HtmlTag;
                case "p":
                    return TagType.PTag;
                case "strong":
                    return TagType.StrongTag;
                case "title":
                    return TagType.TitleTag;
                default:
                    return TagType.CustomTag;
            }
        }

        private ExpressionSyntax ParseText()
        {
            var text = Match(SyntaxKind.LiteralToken);
            return new TextExpressionSyntax(text);
        }

        private ExpressionSyntax ParseEndTag()
        {
            var openBracketSlash = Match(SyntaxKind.LessThanBackslashToken);
            var tagName = Match(SyntaxKind.HtmlKeyword);
            var closeBracket = Match(SyntaxKind.GreaterThanToken);

            return new EndTagExpressionSyntax(openBracketSlash, tagName, closeBracket);
        }

        private ExpressionSyntax ParseStartTag()
        {
            var openBracket = Match(SyntaxKind.LessThanToken);
            var tagName = Match(SyntaxKind.HtmlKeyword);
            var attributes = ParseAttributes();
            var closeBracket = Match(SyntaxKind.GreaterThanToken);

            return new StartTagExpressionSyntax(openBracket, tagName, attributes, closeBracket);
        }

        private AttributeExpressionSyntax[] ParseAttributes()
        {
            var attributes = new List<AttributeExpressionSyntax>();

            while(Current.Kind != SyntaxKind.GreaterThanToken)
            {
                attributes.Add(ParseAttributeExpression());
            }

            return attributes.ToArray();
        }

        private AttributeExpressionSyntax ParseAttributeExpression()
        {
            var attributeName = Match(SyntaxKind.LiteralToken);
            var equalSign = Match(SyntaxKind.EqualsToken);
            var value = ParseStringExpresion();

            return new AttributeExpressionSyntax(attributeName, equalSign, value);
        }

        private StringExpressionSyntax ParseStringExpresion()
        {
            var quoteType = Current.Kind;
            var openQuote = quoteType == SyntaxKind.DoubleQuoteToken ? Match(SyntaxKind.DoubleQuoteToken) : Match(SyntaxKind.SingleQuoteToken);
            var stringContent = new List<SyntaxToken>();
            while (Current.Kind != quoteType)
            {
                stringContent.Add(Match(SyntaxKind.LiteralToken));
            }

            var closeQuote = Match(quoteType);

            return new StringExpressionSyntax(openQuote, stringContent.ToArray(), closeQuote);
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            if (Current.Kind == kind || Current.Kind == SyntaxKind.Any)
                return NextToken();

            return new SyntaxToken(kind, Current.Position, null, null);
        }

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
                return _tokens[^1];

            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);
    }
}
