using CoolHTML.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoolHTML
{
    public class CoolHTMLDocument
    {
        public CoolHTMLDocument()
        {

        }

        public void Parse()
        {
            var parser = new Parser(File.ReadAllText(@"..\..\..\..\CoolHTML\HTML\index.html"));
            while (true)
            {
                var a = parser.Parse();
                //Console.WriteLine(a);
                Debugger.Break();
            }
        }

        //public List<SyntaxToken> FromHtmlString(string html)
        //{


        //    //var lexer = new Lexer(html);
        //    //var syntaxTokenList = new List<SyntaxToken>();
        //    //while (true)
        //    //{
        //    //    var lexed = lexer.Lex();
        //    //    if (lexed.Kind == SyntaxKind.EndOfFileToken) break;

        //    //    syntaxTokenList.Add(lexed);
        //    //}

        //    //return syntaxTokenList;
        //}

        //public List<SyntaxToken> FromFile(string path)
        //{
        //    return FromHtmlString(File.ReadAllText(path));
        //}

        //public async Task<List<SyntaxToken>> FromUri(string url)
        //{
        //    return await FromUri(new Uri(url));
        //}

        //public async Task<List<SyntaxToken>> FromUri(Uri uri)
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var response = await client.GetAsync(uri);
        //        response.EnsureSuccessStatusCode();
        //        var htmlString = await response.Content.ReadAsStringAsync();

        //        return FromHtmlString(htmlString);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
