using System;
using System.Collections.Generic;
using System.Text;

namespace CoolHTML.Syntax
{
    internal partial class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;

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

        public ExpressionSyntax Parse()
        {
            switch (Current.Kind)
            {
                case SyntaxKind.EndOfFileToken:
                    return new EndOfFileExpressionSyntax();
                case SyntaxKind.LessThanToken:
                    return ParseStartTag();
                case SyntaxKind.LessThanBackslashToken:
                    return ParseEndTag();
                default:
                    return ParseText();
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
