using System;
using System.IO;
using System.Linq;
using Sprache;

namespace HtmlParser
{
    class Program
    {
        static Parser<string> TestParser =>
            from q in MonadHtmlParser.FindTag("div", new Attribute("class", "registerBox registerBoxBank margBtm20"))
            select q;

        static void Main(string[] args)
        {
            var text = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "TextFile1.txt"));
            var result = TestParser.Parse(text);
        }
    }
}
