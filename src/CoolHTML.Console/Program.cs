using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using CoolHTML;
using CoolHTML.Syntax;
using Newtonsoft.Json;

namespace CoolHTML.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var a = new CoolHTMLDocument().FromFile(@"C:\Sun Zizhuo\Code\C#\Console C#\CoolHTML\src\CoolHTML\HTML\index.html");
            //var a = new CoolHTMLDocument().FromUri("https://iemb.hci.edu.sg");

            //Print(a);
            new CoolHTMLDocument().Parse();
        }

        private static void Print(List<SyntaxToken> tokens)
        {
            foreach (var item in tokens)
            {
                System.Console.WriteLine($"{{ \n" +
                    $"  Kind: {item.Kind}, \n" +
                    $"  Position: {item.Position}, \n" +
                    $"  Value: {Microsoft.CodeAnalysis.CSharp.SymbolDisplay.FormatLiteral(item.Text, true)}, \n" +
                    $"}},");
            }
        }
    }
}