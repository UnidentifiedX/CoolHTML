using System;

namespace CoolHTML.Syntax
{
    internal class Lexer
    {
        private readonly string _html;
        private int _position;

        public Lexer(string html)
        {
            _html = html;
        }

        private char Current
        {
            get
            {
                if (_position >= _html.Length)
                    return '\0';

                return _html[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        private char Peek(int offset)
        {
            return _html[_position + offset];
        }

        public SyntaxToken Lex()
        {
            if (_position >= _html.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0", null);
            }

            switch (Current)
            {
                case '!':
                    return new SyntaxToken(SyntaxKind.ExclamationToken, _position++, "!", null);
                case '-':
                    if(Peek(1) == '-' && Peek(2) == '>')
                    {
                        return new SyntaxToken(SyntaxKind.HyphenHyphenToken, _position += 3, "-->", null);
                    }
                    goto default;
                case '<':
                    if(Peek(1) == '/')
                    {
                        return new SyntaxToken(SyntaxKind.LessThanBackslashToken, _position += 2, "</", null);
                    }
                    return new SyntaxToken(SyntaxKind.LessThanToken, _position++, "<", null);
                case '>':
                    return new SyntaxToken(SyntaxKind.GreaterThanToken, _position++, ">", null);
                case '"':
                    return new SyntaxToken(SyntaxKind.DoubleQuoteToken, _position++, "\"", null);                
                case '\'':
                    return new SyntaxToken(SyntaxKind.SingleQuoteToken, _position++, "'", null);
                case '=':
                    return new SyntaxToken(SyntaxKind.EqualsToken, _position++, "=", null);
                default:
                    SyntaxKind kind;

                    if (char.IsWhiteSpace(Current))          
                    {
                        var start = _position;

                        while (char.IsWhiteSpace(Current))
                            Next();

                        var length = _position - start;
                        var text = _html.Substring(start, length);

                        return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
                    }
                    else
                    {
                        var start = _position;

                        if (Peek(-1) == '<' || (Peek(-2) == '<' && Peek(-1) == '/'))
                        {
                            while (!char.IsWhiteSpace(Current) && Current != '>')
                                Next();

                            kind = SyntaxKind.HtmlKeyword;
                        }
                        else
                        {
                            while(Current != '>' 
                                && Current != '<' 
                                && Current != '=' 
                                && Current != '"'
                                && Current != '\'')
                                Next();

                            kind = SyntaxKind.LiteralToken;
                        }


                        var length = _position - start;
                        var text = _html.Substring(start, length);

                        return new SyntaxToken(kind, start, text, null);
                    }
            }
        }
    }
}